using Vehicle.Insurance.Core.Contracts.VehicleColors.Commands;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.VehicleColors.Entities;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.Create;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleColors.Commands.Create;

public class CreateVehicleColorHandler : CommandHandler<CreateVehicleColorCommand, long>
{
    private readonly IVehicleColorCommandRepository _commandRepository;
    private readonly Dictionary<bool, Func<CreateVehicleColorCommand, long, VehicleColor, Task<VehicleColor>>> _actions;

    public CreateVehicleColorHandler(ZaminServices zaminServices,
                                IVehicleColorCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
        _actions = new()
        {
            [true] = async (command, nextPriority, vehicleColor) => await Create(command, nextPriority, vehicleColor),
            [false] = async (command, nextPriority, vehicleColor) => await Restore(command, nextPriority, vehicleColor),
        };
    }

    public override async Task<CommandResult<long>> Handle(CreateVehicleColorCommand command)
    {
        var isDuplicateVehicleColor = await _commandRepository
            .ExistsAsync(e => DIPTitle.FromString(command.Title).Equals(e.Title) ||
                              ColorHash.FromString(command.ColorHash).Equals(e.ColorHash) ||
                              CoreId.FromString(command.CoreId).Equals(e.CoreId));

        if (isDuplicateVehicleColor)
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.VEHICLE_COLOR]);

        VehicleColor? vehicleColor = await _commandRepository.GetByCoreIdIgnoreQueryFiltersAsync(command.CoreId);

        long nextPriority = await _commandRepository.GetNextPriority();

        vehicleColor = await _actions[vehicleColor is null](command, nextPriority, vehicleColor);

        await _commandRepository.CommitAsync();

        return Ok(vehicleColor.Id);
    }

    #region Methods
    private async Task<VehicleColor> Create(CreateVehicleColorCommand command, long nextPriority, VehicleColor? vehicleColor)
    {
        vehicleColor = VehicleColor.Create(command.ToCreateParameter(nextPriority));

        await _commandRepository.InsertAsync(vehicleColor);

        return vehicleColor;
    }

    private async Task<VehicleColor> Restore(CreateVehicleColorCommand command, long nextPriority, VehicleColor? vehicleColor)
    {
        vehicleColor?.Restore(command.ToRestoreParameter(nextPriority));

        return vehicleColor;
    }
    #endregion
}



