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

    public CreateVehicleColorHandler(ZaminServices zaminServices,
                                IVehicleColorCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult<long>> Handle(CreateVehicleColorCommand command)
    {
        var isDuplicateVehicleColor = await _commandRepository
            .ExistsAsync(e => DIPTitle.FromString(command.Title).Equals(e.Title) ||
                              CoreId.FromString(command.CoreId).Equals(e.CoreId));

        if (isDuplicateVehicleColor)
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.VEHICLE_COLOR]);

        long nextPriority = await _commandRepository.GetNextPriority();

        var vehicleColor = VehicleColor.Create(command.ToCreateParameter(nextPriority));

        await _commandRepository.InsertAsync(vehicleColor);

        await _commandRepository.CommitAsync();

        return Ok(vehicleColor.Id);
    }
}
