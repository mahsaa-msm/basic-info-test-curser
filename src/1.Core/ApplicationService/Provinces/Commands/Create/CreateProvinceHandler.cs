using Master.Data.Core.Contracts.Provinces.Commands;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.Provinces.Entities;
using Master.Data.Core.RequestResponse.Provinces.Commands.Create;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Provinces.Commands.Create;

public class CreateProvinceHandler : CommandHandler<CreateProvinceCommand, long>
{
    private readonly IProvinceCommandRepository _provinceCommandRepository;
    private readonly Dictionary<bool, Func<CreateProvinceCommand, long, Province, Task<Province>>> _actions;

    public CreateProvinceHandler(ZaminServices zaminServices,
                                IProvinceCommandRepository provinceCommandRepository)
        : base(zaminServices)
    {
        _provinceCommandRepository = provinceCommandRepository;
        _actions = new()
        {
            [true] = async (command, nextPriority, province) => await Create(command, nextPriority, province),
            [false] = async (command, nextPriority, province) => await Restore(command, nextPriority, province),
        };
    }

    public override async Task<CommandResult<long>> Handle(CreateProvinceCommand command)
    {
        var isDuplicateProvince = await _provinceCommandRepository
            .ExistsAsync(e => e.Title == Title.FromString(command.Title) ||
                              e.Code == Code.FromString(command.Code) ||
                              e.CoreId == CoreId.FromString(command.CoreId));

        if (isDuplicateProvince)
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.NAME]);

        Province? province = await _provinceCommandRepository.GetByCoreIdIgnoreQueryFiltersAsync(command.CoreId);

        long nextPriority = await _provinceCommandRepository.GetNextPriority();

        province = await _actions[province is null](command, nextPriority, province);

        await _provinceCommandRepository.CommitAsync();

        return Ok(province.Id);
    }

    #region Methods
    private async Task<Province> Create(CreateProvinceCommand command, long nextPriority, Province? province)
    {
        province = Province.Create(command.ToCreateParameter(nextPriority));

        await _provinceCommandRepository.InsertAsync(province);

        return province;
    }

    private async Task<Province> Restore(CreateProvinceCommand command, long nextPriority, Province? province)
    {
        province?.Restore(command.ToRestoreParameter(nextPriority));

        return province;
    }
    #endregion
}