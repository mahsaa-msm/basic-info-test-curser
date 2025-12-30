using Master.Data.Core.Contracts.Tenants.Comamnds;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Domain.Tenants.Entities;
using Master.Data.Core.Domain.Tenants.Entities.Settings;
using Master.Data.Core.Domain.Tenants.Parameters;
using Master.Data.Core.RequestResponse.Tenants.Commands.UpdateUiConfig;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Tenants.Commands;
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
