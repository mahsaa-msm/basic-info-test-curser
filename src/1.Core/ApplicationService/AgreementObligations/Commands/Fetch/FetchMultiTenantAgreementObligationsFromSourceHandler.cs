using Master.Data.Core.ApplicationService.Common.FinglishConverterService;
using Master.Data.Core.Contracts.AgreementObligations.Commands;
using Master.Data.Core.Contracts.CoreInsuranceApis.Agreements;
using Master.Data.Core.Contracts.Tenants.Queries;
using Master.Data.Core.Domain.AgreementObligations.Entities;
using Master.Data.Core.Domain.AgreementObligations.Parameters;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.RequestResponse.AgreementObligations.Commands.Fetch;
using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.Agreements.GetAllAgreementObligations;
using Master.Data.Core.RequestResponse.Tenants.Queries.GetIAllSelectItem;
using Master.Data.Core.Resources;
using Master.Data.Core.Resources.Utils.Extensions;
using Microsoft.Extensions.Logging;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Common;
using Zamin.Utilities;
using Zamin.Utilities.Extensions;

namespace Master.Data.Core.ApplicationService.AgreementObligations.Commands.Fetch;

public sealed class FetchMultiTenantAgreementObligationsFromSourceHandler : CommandHandler<FetchMultiTenantAgreementObligationsFromSourceCommand>
{
    private readonly IAgreementObligationCommandRepository _agreementObligationCommandRepository;
    private readonly ITenantQueryRepository _tenantQueryRepository;
    private readonly ICoreInsuranceGetAllAgreementObligationsCaller _coreInsuranceGetAllAgreementObligationsCaller;
    private readonly ILogger<FetchMultiTenantAgreementObligationsFromSourceHandler> _logger;
    private readonly IFinglishConverter _finglishConverter;

    public FetchMultiTenantAgreementObligationsFromSourceHandler(ZaminServices zaminServices,
                                                                 IAgreementObligationCommandRepository agreementObligationCommandRepository,
                                                                 ITenantQueryRepository tenantQueryRepository,
                                                                 ICoreInsuranceGetAllAgreementObligationsCaller coreInsuranceGetAllAgreementObligationsCaller,
                                                                 ILogger<FetchMultiTenantAgreementObligationsFromSourceHandler> logger,
                                                                 IFinglishConverter finglishConverter)
        : base(zaminServices)
    {
        _agreementObligationCommandRepository = agreementObligationCommandRepository;
        _tenantQueryRepository = tenantQueryRepository;
        _coreInsuranceGetAllAgreementObligationsCaller = coreInsuranceGetAllAgreementObligationsCaller;
        _logger = logger;
        _finglishConverter = finglishConverter;
    }

    public override async Task<CommandResult> Handle(FetchMultiTenantAgreementObligationsFromSourceCommand command)
    {
        var coreAgreementObligationsResponse = await _coreInsuranceGetAllAgreementObligationsCaller.Call(new GetAllAgreementObligationsRequest());

        if (coreAgreementObligationsResponse.IsFailure ||
            coreAgreementObligationsResponse.Value is null ||
            !coreAgreementObligationsResponse.Value.Any())
        {
            _logger.LogWarning(string.Format(ProjectTranslation.FETCH_DATA_FROM_CORE_FAILED,
                                             ProjectTranslation.AGREEMENT_OBLIGATION));
            return Result(ApplicationServiceStatus.NotFound);
        }

        var tenants = await _tenantQueryRepository.ExecuteAsync(new GetAllTenantsSelectItemQuery());
        if (!tenants.Any())
            return Result(ApplicationServiceStatus.NotFound);

        long nextPriority = await _agreementObligationCommandRepository.GetNextPriority();

        foreach (var tenant in tenants)
        {
            var tenantAgreementObligations = await _agreementObligationCommandRepository.GetByTenantId(tenant.Id);

            foreach (var coreAgreementObligation in coreAgreementObligationsResponse.Value)
            {
                try
                {
                    var tenantAgreementObligation = tenantAgreementObligations
                        .FirstOrDefault(c => CoreId.FromLong(coreAgreementObligation.AgreementObligationCoreId).Equals(c.CoreId));

                    if (tenantAgreementObligation is null)
                    {
                        var newTenantAgreementObligation = AgreementObligation
                            .CreateWithTenantId(new CreateAgreementObligationParameter(coreAgreementObligation.Title,
                                                                                       coreAgreementObligation.Title,
                                                                                       coreAgreementObligation.AgreementObligationCoreId,
                                                                                       coreAgreementObligation.AgreementCoreId,
                                                                                       !string.IsNullOrEmpty(coreAgreementObligation.Code) ?
                                                                                            coreAgreementObligation.Code :
                                                                                            _finglishConverter.Convert(coreAgreementObligation.Title)
                                                                                            .Substring(0, ProjectConsts.CODE_MAX_LENGTH - 1),
                                                                                       coreAgreementObligation.StartDate.ToSafeDateTime(targetKind: DateTimeKind.Utc),
                                                                                       coreAgreementObligation.EndDate.ToSafeDateTime(targetKind: DateTimeKind.Utc),
                                                                                       coreAgreementObligation.PrepaymentPercentage,
                                                                                       coreAgreementObligation.FirstInstallmentDeadline,
                                                                                       coreAgreementObligation.InstallmentsCount,
                                                                                       coreAgreementObligation.InstallmentInterval,
                                                                                       coreAgreementObligation.AgreementObligationNumber,
                                                                                       coreAgreementObligation.AgreementNumber,
                                                                                       coreAgreementObligation.InsuranceTypeCoreId,
                                                                                       coreAgreementObligation.SalesType,
                                                                                       nextPriority,
                                                                                       tenant.Id,
                                                                                       tenant.Key));

                        if (coreAgreementObligation.IssuanceSchemes is not null && coreAgreementObligation.IssuanceSchemes.Any())
                            newTenantAgreementObligation.UpdateIssuanceSchemes(coreAgreementObligation.IssuanceSchemes
                                                            .Select(c => CoreId.FromLong(c.IssuanceSchemeCoreId))
                                                            .ToList());

                        await _agreementObligationCommandRepository.InsertAsync(newTenantAgreementObligation);
                        nextPriority++;
                    }
                    else
                    {
                        if (tenantAgreementObligation.Title.Value != coreAgreementObligation.Title.ApplyCorrectYeKe() ||
                            tenantAgreementObligation.AgreementCoreId.Value != coreAgreementObligation.AgreementCoreId.ToString() ||
                            tenantAgreementObligation.Code.Value != coreAgreementObligation.Code ||
                            tenantAgreementObligation.AgreementNumber != coreAgreementObligation.AgreementNumber ||
                            tenantAgreementObligation.AgreementObligationNumber != coreAgreementObligation.AgreementObligationNumber)
                        {

                            tenantAgreementObligation.Update(new UpdateAgreementObligationParameter(coreAgreementObligation.Title,
                                                                                                    tenantAgreementObligation.DisplayTitle,
                                                                                                    coreAgreementObligation.AgreementCoreId,
                                                                                                    coreAgreementObligation.Code,
                                                                                                    coreAgreementObligation.StartDate
                                                                                                        .ToSafeDateTime(targetKind: DateTimeKind.Utc),
                                                                                                    coreAgreementObligation.EndDate
                                                                                                        .ToSafeDateTime(targetKind: DateTimeKind.Utc),
                                                                                                    coreAgreementObligation.PrepaymentPercentage,
                                                                                                    coreAgreementObligation.FirstInstallmentDeadline,
                                                                                                    coreAgreementObligation.InstallmentsCount,
                                                                                                    coreAgreementObligation.InstallmentInterval,
                                                                                                    coreAgreementObligation.AgreementObligationNumber,
                                                                                                    coreAgreementObligation.AgreementNumber,
                                                                                                    coreAgreementObligation.InsuranceTypeCoreId,
                                                                                                    coreAgreementObligation.SalesType,
                                                                                                    tenantAgreementObligation.Priority));

                            if (coreAgreementObligation.IssuanceSchemes is not null && coreAgreementObligation.IssuanceSchemes.Any())
                                tenantAgreementObligation.UpdateIssuanceSchemes(coreAgreementObligation.IssuanceSchemes
                                                                                    .Select(c => CoreId.FromLong(c.IssuanceSchemeCoreId))
                                                                                    .ToList());
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            await _agreementObligationCommandRepository.CommitAsync();
        }

        await _agreementObligationCommandRepository.CommitAsync();
        return Ok();
    }
}