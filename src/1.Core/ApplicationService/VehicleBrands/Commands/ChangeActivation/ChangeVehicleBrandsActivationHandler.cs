using Vehicle.Insurance.Core.Contracts.VehicleBrands.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.VehicleBrands.Entities;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Commands.ChangeActivation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleBrands.Commands.ChangeActivation;

public sealed class ChangeVehicleBrandsActivationHandler : CommandHandler<ChangeVehicleBrandsActivationCommand>
{
    private readonly IVehicleBrandCommandRepository _commandRepository;

    private static readonly Dictionary<bool, Action<List<VehicleBrand>>> Actions = new()
    {
        [true] = list => list.ForEach(e => e.Active()),
        [false] = list => list.ForEach(e => e.Deactive()),
    };

    public ChangeVehicleBrandsActivationHandler(ZaminServices zaminServices,
        IVehicleBrandCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(ChangeVehicleBrandsActivationCommand command)
    {
        var list = await _commandRepository.GetByIds(command.VehicleBrandsId);
        EntityGuard.ThrowIfListIsEmptyWithLongId(list, ProjectTranslation.VEHICLE_BRAND);
        Actions[command.IsActive](list);
        await _commandRepository.CommitAsync();
        return Ok();
    }
}
