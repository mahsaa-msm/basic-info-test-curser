using Vehicle.Insurance.Core.Contracts.Tenants.Comamnds;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.Tenants.Entities;
using Vehicle.Insurance.Core.Domain.Tenants.Entities.Settings;
using Vehicle.Insurance.Core.Domain.Tenants.Parameters;
using Vehicle.Insurance.Core.RequestResponse.Tenants.Commands.UpdateUiConfig;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.Tenants.Commands;

public sealed class UpdateUiTenantConfigHandler : CommandHandler<UpdateUiTenantConfigCommand>
{
    private readonly ITenantCommandRepository _tenantCommandRepository;

    public UpdateUiTenantConfigHandler(ZaminServices zaminServices,
                                     ITenantCommandRepository tenantCommandRepository)
        : base(zaminServices)
    {
        _tenantCommandRepository = tenantCommandRepository;
    }

    public override async Task<CommandResult> Handle(UpdateUiTenantConfigCommand command)
    {
        var tenant = await _tenantCommandRepository.GetAsync(command.TenantId);
        EntityGuard.ThrowIfNull<Tenant, long>(tenant, ProjectTranslation.TENANT);

        var uiSettings = UISettings.Create(new CreateUiSettingsParameters(command.Theme));

        tenant.UpdateConfig(uiSettings);
        await _tenantCommandRepository.CommitAsync();
        return Ok();
    }
}

