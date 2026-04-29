using Vehicle.Insurance.Core.Contracts.Tenants.Comamnds;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.RequestResponse.Tenants.Commands.Update;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.Tenants.Commands;

public sealed class UpdateTenantNameHandler : CommandHandler<UpdateTenantNameCommand>
{
    private readonly ITenantCommandRepository _tenantCommandRepository;

    public UpdateTenantNameHandler(ZaminServices zaminServices,
                                   ITenantCommandRepository tenantCommandRepository)
        : base(zaminServices)
    {
        _tenantCommandRepository = tenantCommandRepository;
    }

    public override async Task<CommandResult> Handle(UpdateTenantNameCommand command)
    {
        var tenant = await _tenantCommandRepository.GetAsync(command.TenantId);
        EntityGuard.ThrowIfNullWithLongId(tenant, ProjectTranslation.TENANT);

        tenant.UpdateName(command.Name);
        await _tenantCommandRepository.CommitAsync();

        return Ok();
    }
}

