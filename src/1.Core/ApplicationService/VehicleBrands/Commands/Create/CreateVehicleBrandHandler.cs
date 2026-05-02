using Vehicle.Insurance.Core.Contracts.VehicleBrands.Commands;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.VehicleBrands.Entities;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Commands.Create;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleBrands.Commands.Create;

public sealed class CreateVehicleBrandHandler : CommandHandler<CreateVehicleBrandCommand, long>
{
    private readonly IVehicleBrandCommandRepository _commandRepository;

    public CreateVehicleBrandHandler(ZaminServices zaminServices,
        IVehicleBrandCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult<long>> Handle(CreateVehicleBrandCommand command)
    {
        var duplicate = await _commandRepository.ExistsAsync(e =>
            DIPTitle.FromString(command.Title).Equals(e.Title) ||
            CoreId.FromString(command.CoreId).Equals(e.CoreId));

        if (duplicate)
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                ProjectTranslation.VEHICLE_BRAND]);

        var nextPriority = await _commandRepository.GetNextPriority();
        var entity = VehicleBrand.Create(command.ToCreateParameter(nextPriority));
        await _commandRepository.InsertAsync(entity);
        await _commandRepository.CommitAsync();
        return Ok(entity.Id);
    }
}
