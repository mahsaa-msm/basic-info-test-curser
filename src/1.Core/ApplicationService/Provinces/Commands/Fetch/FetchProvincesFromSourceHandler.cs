using Vehicle.Insurance.Core.ApplicationService.Common.FinglishConverterService;
using Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.Provinces;
using Vehicle.Insurance.Core.Contracts.Provinces.Commands;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.Provinces.Entities;
using Vehicle.Insurance.Core.Domain.Provinces.Parameters;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.Province.GetAll;
using Vehicle.Insurance.Core.RequestResponse.Provinces.Commands.Fetch;
using Vehicle.Insurance.Core.Resources;
using Microsoft.Extensions.Logging;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Common;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.Provinces.Commands.Fetch;

public sealed class FetchProvincesFromSourceHandler : CommandHandler<FetchProvincesFromSourceCommand>
{
    private readonly IProvinceCommandRepository _provinceCommandRepository;
    private readonly ICoreInsuranceGetAllProvincesCaller _coreInsuranceGetAllProvincesCaller;
    private readonly ILogger<FetchProvincesFromSourceHandler> _logger;
    private readonly IFinglishConverter _finglishConverter;

    public FetchProvincesFromSourceHandler(ZaminServices zaminServices,
                                           IProvinceCommandRepository provinceCommandRepository,
                                           ICoreInsuranceGetAllProvincesCaller coreInsuranceGetAllProvincesCaller,
                                           ILogger<FetchProvincesFromSourceHandler> logger,
                                           IFinglishConverter finglishConverter)
        : base(zaminServices)
    {
        _provinceCommandRepository = provinceCommandRepository;
        _coreInsuranceGetAllProvincesCaller = coreInsuranceGetAllProvincesCaller;
        _logger = logger;
        _finglishConverter = finglishConverter;
    }

    public override async Task<CommandResult> Handle(FetchProvincesFromSourceCommand command)
    {
        var coreProvincesResponse = await _coreInsuranceGetAllProvincesCaller.Call(new GetAllProvincesRequest());

        if (coreProvincesResponse.IsFailure ||
            coreProvincesResponse.Value is null ||
            !coreProvincesResponse.Value.Any())
        {
            _logger.LogWarning(string.Format(ProjectTranslation.FETCH_DATA_FROM_CORE_FAILED,
                                             ProjectTranslation.PROVINCE));
            return Result(ApplicationServiceStatus.NotFound);
        }

        var provinces = await _provinceCommandRepository.GetAllAsync();
        long nextPriority = await _provinceCommandRepository.GetNextPriority();

        foreach (var coreProvince in coreProvincesResponse.Value.DistinctBy(c => c.ostanID))
        {
            try
            {
                var province = provinces
                        .FirstOrDefault(c => c.CoreId == CoreId.FromLong(coreProvince.ostanID));

                if (province is null)
                {
                    var newProvince = Province.Create(new CreateProvinceParameter(coreProvince.naamOstan,
                                                                                  coreProvince.naamOstan,
                                                                                  coreProvince.ostanID,
                                                                                  !string.IsNullOrEmpty(coreProvince.codeOstan) ?
                                                                                      coreProvince.codeOstan :
                                                                                      _finglishConverter.Convert(coreProvince.naamOstan),
                                                                                  nextPriority,
                                                                                  coreProvince.keshvarID));
                    await _provinceCommandRepository.InsertAsync(newProvince);
                    nextPriority++;
                }
                else
                {
                    if (!DIPTitle.FromString(coreProvince.naamOstan).Equals(province.Title) ||
                        !CoreId.FromLong(coreProvince.keshvarID).Equals(province.CountryCoreId))
                        province.Update(new UpdateProvinceParameter(coreProvince.naamOstan,
                                                                    province.DisplayTitle,
                                                                    !string.IsNullOrEmpty(coreProvince.codeOstan) ?
                                                                        coreProvince.codeOstan :
                                                                        _finglishConverter.Convert(coreProvince.naamOstan),
                                                                    province.Priority,
                                                                    coreProvince.keshvarID));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        await _provinceCommandRepository.CommitAsync();

        return Ok();
    }
}
