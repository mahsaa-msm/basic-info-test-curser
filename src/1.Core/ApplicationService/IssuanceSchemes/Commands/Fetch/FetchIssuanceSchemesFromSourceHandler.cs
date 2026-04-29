using Vehicle.Insurance.Core.ApplicationService.Common.FinglishConverterService;
using Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.IssuanceSchemes;
using Vehicle.Insurance.Core.Contracts.IssuanceSchemes.Commands;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.IssuanceSchemes.Entities;
using Vehicle.Insurance.Core.Domain.IssuanceSchemes.Parameters;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.IssuanceScheme.GetAll;
using Vehicle.Insurance.Core.RequestResponse.IssuanceSchemes.Commands.Fetch;
using Vehicle.Insurance.Core.Resources;
using Microsoft.Extensions.Logging;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Common;
using Zamin.Utilities;
using Zamin.Utilities.Extensions;

namespace Vehicle.Insurance.Core.ApplicationService.IssuanceSchemes.Commands.Fetch;
public sealed class FetchIssuanceSchemesFromSourceHandler : CommandHandler<FetchIssuanceSchemesFromSourceCommand>
{
    private readonly IIssuanceSchemeCommandRepository _commandRepository;
    private readonly ICoreInsuranceGetAllIssuanceSchemesCaller _coreInsuranceGetAllIssuanceSchemesCaller;
    private readonly ILogger<FetchIssuanceSchemesFromSourceHandler> _logger;
    private readonly IFinglishConverter _finglishConverter;

    public FetchIssuanceSchemesFromSourceHandler(ZaminServices zaminServices,
                                           IIssuanceSchemeCommandRepository commandRepository,
                                           ICoreInsuranceGetAllIssuanceSchemesCaller coreInsuranceGetAllIssuanceSchemesCaller,
                                           ILogger<FetchIssuanceSchemesFromSourceHandler> logger,
                                           IFinglishConverter finglishConverter)
        : base(zaminServices)
    {
        _commandRepository = commandRepository;
        _coreInsuranceGetAllIssuanceSchemesCaller = coreInsuranceGetAllIssuanceSchemesCaller;
        _logger = logger;
        _finglishConverter = finglishConverter;
    }

    public override async Task<CommandResult> Handle(FetchIssuanceSchemesFromSourceCommand command)
    {
        var coreIssuanceSchemesResponse = await _coreInsuranceGetAllIssuanceSchemesCaller.Call(new GetAllIssuanceSchemesRequest());

        if (coreIssuanceSchemesResponse.IsFailure ||
            coreIssuanceSchemesResponse.Value is null ||
            !coreIssuanceSchemesResponse.Value.Any())
        {
            _logger.LogWarning(string.Format(ProjectTranslation.FETCH_DATA_FROM_CORE_FAILED,
                                             ProjectTranslation.ISSUANCE_SCHEME));

            return Result(ApplicationServiceStatus.NotFound);
        }

        var issuanceSchemes = await _commandRepository.GetAllAsync();
        long nextPriority = await _commandRepository.GetNextPriority();

        foreach (var coreIssuanceScheme in coreIssuanceSchemesResponse.Value)
        {
            var issuanceScheme = issuanceSchemes
                .FirstOrDefault(c => c.CoreId == CoreId.FromLong(coreIssuanceScheme.tarhSodoorID));


            if (issuanceScheme is null)
            {
                var newIssuanceScheme = IssuanceScheme.Create(new CreateIssuanceSchemeParameter(coreIssuanceScheme.naam,
                                                                                                coreIssuanceScheme.naam,
                                                                                                coreIssuanceScheme.tarhSodoorID,
                                                                                                !string.IsNullOrEmpty(coreIssuanceScheme.code) ? coreIssuanceScheme.code : _finglishConverter.Convert(coreIssuanceScheme.naam),
                                                                                                coreIssuanceScheme.tarikhShorooAz is not null ? coreIssuanceScheme.tarikhShorooAz.ToUtcDateTime() : null,
                                                                                                coreIssuanceScheme.tarikhShorooTa is not null ? coreIssuanceScheme.tarikhShorooTa.ToUtcDateTime() : null,
                                                                                                coreIssuanceScheme.azTarikhSodoor is not null ? coreIssuanceScheme.azTarikhSodoor.ToUtcDateTime() : null,
                                                                                                coreIssuanceScheme.taTarikhSodoor is not null ? coreIssuanceScheme.taTarikhSodoor.ToUtcDateTime() : null,
                                                                                                coreIssuanceScheme.noeBimehID,
                                                                                                coreIssuanceScheme.takhfifEzafeh,
                                                                                                coreIssuanceScheme.darsadTakhfifEzafeh,
                                                                                                coreIssuanceScheme.faal,
                                                                                                nextPriority));
                await _commandRepository.InsertAsync(newIssuanceScheme);
                nextPriority++;
            }
            else
            {
                if (issuanceScheme.Title != DIPTitle.FromString(coreIssuanceScheme.naam))
                    issuanceScheme.Update(new UpdateIssuanceSchemeParameter(coreIssuanceScheme.naam,
                                                                            issuanceScheme.DisplayTitle,
                                                                            !string.IsNullOrEmpty(coreIssuanceScheme.code) ? coreIssuanceScheme.code : _finglishConverter.Convert(coreIssuanceScheme.naam),
                                                                            coreIssuanceScheme.tarikhShorooAz is not null ? coreIssuanceScheme.tarikhShorooAz.ToUtcDateTime() : null,
                                                                            coreIssuanceScheme.tarikhShorooTa is not null ? coreIssuanceScheme.tarikhShorooTa.ToUtcDateTime() : null,
                                                                            coreIssuanceScheme.azTarikhSodoor is not null ? coreIssuanceScheme.azTarikhSodoor.ToUtcDateTime() : null,
                                                                            coreIssuanceScheme.taTarikhSodoor is not null ? coreIssuanceScheme.taTarikhSodoor.ToUtcDateTime() : null,
                                                                            coreIssuanceScheme.noeBimehID,
                                                                            coreIssuanceScheme.takhfifEzafeh,
                                                                            coreIssuanceScheme.darsadTakhfifEzafeh,
                                                                            issuanceScheme.Priority));

            }
        }

        await _commandRepository.CommitAsync();

        return Ok();
    }
}
