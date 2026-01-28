using Master.Data.Core.Contracts.Tenants.Comamnds;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Domain.Tenants.Entities;
using Master.Data.Core.Domain.Tenants.Entities.Settings;
using Master.Data.Core.Domain.Tenants.Parameters;
using Master.Data.Core.RequestResponse.Tenants.Commands.AddSsoConfig;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Tenants.Commands;

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
