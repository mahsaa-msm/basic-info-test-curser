using Vehicle.Insurance.Core.Contracts.VehicleTips.Commands;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.VehicleTips.Entities;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Commands.Create;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleTips.Commands.Create;

public sealed class CreateVehicleTipHandler : CommandHandler<CreateVehicleTipCommand, long>
{
    private readonly IVehicleTipCommandRepository _commandRepository;

    public CreateVehicleTipHandler(ZaminServices zaminServices,
        IVehicleTipCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult<long>> Handle(CreateVehicleTipCommand command)
    {
        var duplicate = await _commandRepository.ExistsAsync(e =>
            DIPTitle.FromString(command.Title).Equals(e.Title) ||
            CoreId.FromString(command.CoreId).Equals(e.CoreId));

        if (duplicate)
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                ProjectTranslation.VEHICLE_TIP]);

        var nextPriority = await _commandRepository.GetNextPriority();
        var entity = VehicleTip.Create(command.ToCreateParameter(nextPriority));
        await _commandRepository.InsertAsync(entity);
        await _commandRepository.CommitAsync();
        return Ok(entity.Id);
    }
}
