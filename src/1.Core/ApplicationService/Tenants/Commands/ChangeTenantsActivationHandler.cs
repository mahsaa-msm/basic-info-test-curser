using Vehicle.Insurance.Core.Contracts.Tenants.Comamnds;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.Tenants.Entities;
using Vehicle.Insurance.Core.RequestResponse.Tenants.Commands.ChangeActivation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.Tenants.Commands;

public sealed class ChangeTenantsActivationHandler : CommandHandler<ChangeTenantsActivationCommand>
{
    private readonly ITenantCommandRepository _tenantCommandRepository;
    private static readonly Dictionary<bool, Action<List<Tenant>>> _actions = new()
    {
        [true] = c => c.ForEach(c => c.Activate()),
        [false] = c => c.ForEach(c => c.Deactivate())
    };

    public ChangeTenantsActivationHandler(ZaminServices zaminServices,
                                          ITenantCommandRepository tenantCommandRepository)
        : base(zaminServices)
    {
        _tenantCommandRepository = tenantCommandRepository;
    }

    public override async Task<CommandResult> Handle(ChangeTenantsActivationCommand command)
    {
        List<Tenant> tenants = await _tenantCommandRepository.GetByIds(command.TenantIds);

        EntityGuard.ThrowIfListIsEmptyWithLongId(tenants, ProjectTranslation.TENANT);

        _actions[command.IsActive](tenants);
        await _tenantCommandRepository.CommitAsync();

        return Ok();

    }
}

