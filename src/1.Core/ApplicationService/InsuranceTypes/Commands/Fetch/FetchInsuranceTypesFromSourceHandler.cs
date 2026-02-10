using Master.Data.Core.ApplicationService.Common.FinglishConverterService;
using Master.Data.Core.Contracts.CoreInsuranceApis.InsuranceTypes;
using Master.Data.Core.Contracts.InsuranceTypes.Commands;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.InsuranceTypes.Entities;
using Master.Data.Core.Domain.InsuranceTypes.Parameters;
using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.InsuranceType.GetAll;
using Master.Data.Core.RequestResponse.InsuranceTypes.Commands.Fetch;
using Master.Data.Core.Resources;
using Microsoft.Extensions.Logging;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Common;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.InsuranceTypes.Commands.Fetch;

public sealed class FetchInsuranceTypesFromSourceHandler : CommandHandler<FetchInsuranceTypesFromSourceCommand>
{
    private readonly IInsuranceTypeCommandRepository _commandRepository;
    private readonly ICoreInsuranceGetAllInsuranceTypesCaller _coreInsuranceGetAllInsuranceTypesCaller;
    private readonly ILogger<FetchInsuranceTypesFromSourceHandler> _logger;
    private readonly IFinglishConverter _finglishConverter;

    public FetchInsuranceTypesFromSourceHandler(ZaminServices zaminServices,
                                           IInsuranceTypeCommandRepository commandRepository,
                                           ICoreInsuranceGetAllInsuranceTypesCaller coreInsuranceGetAllInsuranceTypesCaller,
                                           ILogger<FetchInsuranceTypesFromSourceHandler> logger,
                                           IFinglishConverter finglishConverter)
        : base(zaminServices)
    {
        _commandRepository = commandRepository;
        _coreInsuranceGetAllInsuranceTypesCaller = coreInsuranceGetAllInsuranceTypesCaller;
        _logger = logger;
        _finglishConverter = finglishConverter;
    }

    public override async Task<CommandResult> Handle(FetchInsuranceTypesFromSourceCommand command)
    {
        var coreInsuranceTypesResponse = await _coreInsuranceGetAllInsuranceTypesCaller.Call(new GetAllInsuranceTypesRequest());

        if (coreInsuranceTypesResponse.IsFailure ||
            coreInsuranceTypesResponse.Value is null ||
            !coreInsuranceTypesResponse.Value.Any())
        {
            _logger.LogWarning(string.Format(ProjectTranslation.FETCH_DATA_FROM_CORE_FAILED,
                                             ProjectTranslation.INSURANCE_TYPE));
            return Result(ApplicationServiceStatus.NotFound);
        }

        var insuranceTypes = await _commandRepository.GetAllAsync();
        long nextPriority = await _commandRepository.GetNextPriority();

        foreach (var coreInsuranceType in coreInsuranceTypesResponse.Value)
        {
            var insuranceType = insuranceTypes
                .FirstOrDefault(c => c.CoreId == CoreId.FromLong(coreInsuranceType.anvaBimehID));

            if (insuranceType is null)
            {
                var newInsuranceType = InsuranceType.Create(new CreateInsuranceTypeParameter(coreInsuranceType.noeBimeh,
                                                                           coreInsuranceType.noeBimeh,
                                                                           coreInsuranceType.anvaBimehID,
                                                                           !string.IsNullOrEmpty(coreInsuranceType.code) ?
                                                                               coreInsuranceType.code :
                                                                               _finglishConverter.Convert(coreInsuranceType.noeBimeh),
                                                                           nextPriority));
                await _commandRepository.InsertAsync(newInsuranceType);
                nextPriority++;
            }
            else
            {
                if (!DIPTitle.FromString(coreInsuranceType.noeBimeh).Equals(insuranceType.Title))
                    insuranceType.Update(new UpdateInsuranceTypeParameter(coreInsuranceType.noeBimeh,
                                                              insuranceType.DisplayTitle,
                                                              !string.IsNullOrEmpty(coreInsuranceType.code) ?
                                                                  coreInsuranceType.code :
                                                                  _finglishConverter.Convert(coreInsuranceType.noeBimeh),
                                                              insuranceType.Priority));
            }
        }

        await _commandRepository.CommitAsync();

        return Ok();
    }
}