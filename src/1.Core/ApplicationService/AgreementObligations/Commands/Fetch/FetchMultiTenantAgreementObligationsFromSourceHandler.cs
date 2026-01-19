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
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Common;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.AgreementObligations.Commands.Fetch;

public sealed class FetchMultiTenantAgreementObligationsFromSourceHandler : CommandHandler<FetchAgreementObligationsFromSourceCommand>
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

    public override async Task<CommandResult> Handle(FetchAgreementObligationsFromSourceCommand command)
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
        long nextPriority = await _agreementObligationCommandRepository.GetNextPriority();

        foreach (var tenant in tenants)
        {
            var tenantAgreementObligations = await _agreementObligationCommandRepository.GetAllAsync();

            foreach (var coreAgreementObligation in coreAgreementObligationsResponse.Value)
            {
                var tenantAgreementObligation = tenantAgreementObligations
                    .FirstOrDefault(c => c.CoreId == CoreId.FromLong(coreAgreementObligation.AgreementObligationCoreId));

                if (tenantAgreementObligation is null)
                {
                    var newTenantAgreementObligation = AgreementObligation
                        .Create(new CreateAgreementObligationParameter(coreAgreementObligation.Title,
                                                                       _finglishConverter.Convert(coreAgreementObligation.Title),
                                                                       coreAgreementObligation.AgreementObligationCoreId,
                                                                       coreAgreementObligation.AgreementCoreId,
                                                                       coreAgreementObligation.Code,
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
                                                                       nextPriority));

                    if (coreAgreementObligation.IssuanceSchemes.Any())
                        foreach (var issuanceScheme in coreAgreementObligation.IssuanceSchemes)
                        {
                            newTenantAgreementObligation.AddIssuanceScheme(issuanceScheme.IssuanceSchemeCoreId);
                        }

                    await _agreementObligationCommandRepository.InsertAsync(newTenantAgreementObligation);
                    nextPriority++;
                }
                else
                {
                    if (tenantAgreementObligation.Title != Title.FromString(coreAgreementObligation.Title) ||
                        tenantAgreementObligation.AgreementCoreId != CoreId.FromLong(coreAgreementObligation.AgreementCoreId) ||
                        tenantAgreementObligation.Code != Code.FromString(coreAgreementObligation.Code) ||
                        tenantAgreementObligation.AgreementNumber != coreAgreementObligation.AgreementNumber ||
                        tenantAgreementObligation.AgreementObligationNumber != coreAgreementObligation.AgreementObligationNumber)
                    {

                        tenantAgreementObligation.Update(new UpdateAgreementObligationParameter(coreAgreementObligation.Title,
                                                                                          tenantAgreementObligation.DisplayTitle,
                                                                                          coreAgreementObligation.AgreementCoreId,
                                                                                          coreAgreementObligation.Code,
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
                                                                                          tenantAgreementObligation.Priority));
                        if (coreAgreementObligation.IssuanceSchemes.Any())
                            foreach (var issuanceScheme in coreAgreementObligation.IssuanceSchemes)
                            {
                                tenantAgreementObligation.AddIssuanceScheme(issuanceScheme.IssuanceSchemeCoreId);
                            }
                    }
                }
            }
        }

        await _agreementObligationCommandRepository.CommitAsync();

        return Ok();
    }
}