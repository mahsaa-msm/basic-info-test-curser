using Master.Data.Core.Contracts.Tenants.Comamnds;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Domain.Tenants.Entities;
using Master.Data.Core.RequestResponse.Tenants.Commands.ChangeActivation;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Tenants.Commands;

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
