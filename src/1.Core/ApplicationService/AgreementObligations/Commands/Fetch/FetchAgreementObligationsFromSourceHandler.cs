using Master.Data.Core.ApplicationService.Common.FinglishConverterService;
using Master.Data.Core.Contracts.AgreementObligations.Commands;
using Master.Data.Core.Contracts.CoreInsuranceApis.Agreements;
using Master.Data.Core.Domain.AgreementObligations.Entities;
using Master.Data.Core.Domain.AgreementObligations.Parameters;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.RequestResponse.AgreementObligations.Commands.Fetch;
using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.Agreements.GetAllAgreementObligations;
using Master.Data.Core.Resources;
using Master.Data.Core.Resources.Utils.Extensions;
using Microsoft.Extensions.Logging;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Common;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.AgreementObligations.Commands.Fetch;

public sealed class FetchAgreementObligationsFromSourceHandler : CommandHandler<FetchAgreementObligationsFromSourceCommand>
{
    private readonly IAgreementObligationCommandRepository _agreementObligationCommandRepository;
    private readonly ICoreInsuranceGetAllAgreementObligationsCaller _coreInsuranceGetAllAgreementObligationsCaller;
    private readonly ILogger<FetchAgreementObligationsFromSourceHandler> _logger;
    private readonly IFinglishConverter _finglishConverter;

    public FetchAgreementObligationsFromSourceHandler(ZaminServices zaminServices,
                                        IAgreementObligationCommandRepository agreementObligationCommandRepository,
                                        ICoreInsuranceGetAllAgreementObligationsCaller coreInsuranceGetAllAgreementObligationsCaller,
                                        ILogger<FetchAgreementObligationsFromSourceHandler> logger,
                                        IFinglishConverter finglishConverter)
        : base(zaminServices)
    {
        _agreementObligationCommandRepository = agreementObligationCommandRepository;
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

        var agreementObligations = await _agreementObligationCommandRepository.GetAllAsync();
        long nextPriority = await _agreementObligationCommandRepository.GetNextPriority();

        foreach (var coreAgreementObligation in coreAgreementObligationsResponse.Value)
        {
            var agreementObligation = agreementObligations
                .FirstOrDefault(c => c.CoreId == CoreId.FromLong(coreAgreementObligation.AgreementObligationCoreId));

            if (agreementObligation is null)
            {
                var newAgreementObligation = AgreementObligation
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
                        newAgreementObligation.AddIssuanceScheme(issuanceScheme.IssuanceSchemeCoreId);
                    }

                await _agreementObligationCommandRepository.InsertAsync(newAgreementObligation);
                nextPriority++;
            }
            else
            {
                if (agreementObligation.Title != Title.FromString(coreAgreementObligation.Title) ||
                    agreementObligation.AgreementCoreId != CoreId.FromLong(coreAgreementObligation.AgreementCoreId) ||
                    agreementObligation.Code != Code.FromString(coreAgreementObligation.Code) ||
                    agreementObligation.AgreementNumber != coreAgreementObligation.AgreementNumber ||
                    agreementObligation.AgreementObligationNumber != coreAgreementObligation.AgreementObligationNumber)
                {

                    agreementObligation.Update(new UpdateAgreementObligationParameter(coreAgreementObligation.Title,
                                                                                      agreementObligation.DisplayTitle,
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
                                                                                      agreementObligation.Priority));
                    if (coreAgreementObligation.IssuanceSchemes.Any())
                        foreach (var issuanceScheme in coreAgreementObligation.IssuanceSchemes)
                        {
                            agreementObligation.AddIssuanceScheme(issuanceScheme.IssuanceSchemeCoreId);
                        }
                }
            }
        }

        await _agreementObligationCommandRepository.CommitAsync();

        return Ok();
    }
}