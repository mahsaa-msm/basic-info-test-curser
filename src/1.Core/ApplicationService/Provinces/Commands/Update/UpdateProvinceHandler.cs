using Vehicle.Insurance.Core.Contracts.Provinces.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.Provinces.Entities;
using Vehicle.Insurance.Core.RequestResponse.Provinces.Commands.Update;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.ApplicationService.Provinces.Commands.Update;

public class UpdateProvinceHandler : CommandHandler<UpdateProvinceCommand>
{
    private readonly IProvinceCommandRepository _provinceCommandRepository;
    private readonly Dictionary<MoveDirection, Func<Province, UpdateProvinceCommand, Task>> _actions;

    public UpdateProvinceHandler(ZaminServices zaminServices,
                                IProvinceCommandRepository provinceCommandRepository) : base(zaminServices)
    {
        _provinceCommandRepository = provinceCommandRepository;
        _actions = new()
        {
            [MoveDirection.Up] = async (province, command) => (await _provinceCommandRepository.GetSuperiorProvinces(province.Priority, command.Priority)).ForEach(c => c.PushDown()),
            [MoveDirection.Down] = async (province, command) => (await _provinceCommandRepository.GetSubordinateProvinces(province.Priority, command.Priority)).ForEach(c => c.PullUp()),
        };
    }

    public override async Task<CommandResult> Handle(UpdateProvinceCommand command)
    {
        Province province = await _provinceCommandRepository.GetAsync(command.ProvinceId);

        EntityGuard.ThrowIfNullWithLongId(province, ProjectTranslation.PROVINCE);

        if (!_provinceCommandRepository.IsCreatedByCore(province))
            await ValidateTitleAndCode(command);

        else
        {
            command.Title = province.Title.Value;
            command.Code = province.Code.Value;
        }

        await CheckPriority(command);

        await MoveProvincesIfNeeded(province, command);

        province.Update(command.ToParameter());

        await _provinceCommandRepository.CommitAsync();

        return Ok();
    }

    #region Methods
    private async Task CheckPriority(UpdateProvinceCommand command)
    {
        var nextPriority = await _provinceCommandRepository.GetNextPriority();
        if (command.Priority > nextPriority - 1)
            command.Priority = nextPriority - 1;
    }
    private async Task MoveProvincesIfNeeded(Province current, UpdateProvinceCommand command)
    {
        var moveDirection = current.GetMoveDirection(command.Priority);
        if (moveDirection != MoveDirection.NoChange)
        {
            await _actions[moveDirection](current, command);
        }
    }
    private async Task ValidateTitleAndCode(UpdateProvinceCommand command)
    {
        ValueObjectGuard.ThrowIfNull(command.Title, ProjectTranslation.TITLE);
        ValueObjectGuard.ThrowIfNull(command.Code, ProjectTranslation.CODE);

        if (await _provinceCommandRepository.ExistsAsync(c => c.Id != command.ProvinceId &&
                                                      (Code.FromString(command.Code).Equals(c.Code) || DIPTitle.FromString(command.Title).Equals(c.Title))))
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.PROVINCE]);
    }
    #endregion
}

