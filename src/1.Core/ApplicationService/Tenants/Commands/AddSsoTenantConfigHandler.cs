using Vehicle.Insurance.Core.Contracts.Tenants.Comamnds;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.Tenants.Entities;
using Vehicle.Insurance.Core.Domain.Tenants.Entities.Settings;
using Vehicle.Insurance.Core.Domain.Tenants.Parameters;
using Vehicle.Insurance.Core.RequestResponse.Tenants.Commands.AddSsoConfig;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.Tenants.Commands;

public sealed class AddSsoTenantConfigHandler : CommandHandler<AddSsoTenantConfigCommand>
{
    private readonly ITenantCommandRepository _tenantCommandRepository;

    public AddSsoTenantConfigHandler(ZaminServices zaminServices,
                                     ITenantCommandRepository tenantCommandRepository)
        : base(zaminServices)
    {
        _tenantCommandRepository = tenantCommandRepository;
    }

    public override async Task<CommandResult> Handle(AddSsoTenantConfigCommand command)
    {
        var tenant = await _tenantCommandRepository.GetAsync(command.TenantId);
        EntityGuard.ThrowIfNull<Tenant, long>(tenant, ProjectTranslation.TENANT);

        var ssoSettings = SsoSettings.Create(new CreateSsoSettingsParameters(command.SsoBasePath,
                                                                             command.UserName,
                                                                             command.Password,
                                                                             command.OauthType));

        tenant.AddConfig(ssoSettings);
        await _tenantCommandRepository.CommitAsync();
        return Ok();
    }
}

