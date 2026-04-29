using Vehicle.Insurance.Core.Contracts.VehicleColors.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.VehicleColors.Entities;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.Delete;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleColors.Commands.Delete;

public class DeleteVehicleColorHandler : CommandHandler<DeleteVehicleColorCommand>
{
    private readonly IVehicleColorCommandRepository _commandRepository;

    public DeleteVehicleColorHandler(ZaminServices zaminServices,
                                IVehicleColorCommandRepository commandRepository)
        : base(zaminServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(DeleteVehicleColorCommand command)
    {
        var vehicleColor = await _commandRepository.GetAsync(command.VehicleColorId);
        EntityGuard.ThrowIfNull<VehicleColor, long>(vehicleColor, ProjectTranslation.VEHICLE_COLOR);

        vehicleColor.Delete();

        var subordinates = await _commandRepository.GetSubordinateVehicleColors(vehicleColor.Priority);

        subordinates?.ForEach(vehicleColor => vehicleColor.PullUp());

        await _commandRepository.CommitAsync();

        return Ok();
    }
}

