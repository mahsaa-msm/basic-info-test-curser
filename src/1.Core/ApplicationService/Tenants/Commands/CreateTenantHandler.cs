using Master.Data.Core.Contracts.Tenants.Comamnds;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.Tenants.Entities;
using Master.Data.Core.RequestResponse.Tenants.Commands.Create;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Tenants.Commands;
public sealed class CreateTenantHandler : CommandHandler<CreateTenantCommand, long?>
{
    private readonly ITenantCommandRepository _tenantCommandRepository;

    public CreateTenantHandler(ZaminServices zaminServices,
                                     ITenantCommandRepository tenantCommandRepository)
        : base(zaminServices)
    {
        _tenantCommandRepository = tenantCommandRepository;
    }

    public override async Task<CommandResult<long?>> Handle(CreateTenantCommand command)
    {
        var tenantExist = await _tenantCommandRepository.ExistsAsync(c => c.Name == Name.FromString(command.Name));
        if (tenantExist)
            throw new ApplicationException(string.Format(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE],
                                                         ProjectTranslation.TENANT));

        var tenant = Tenant.Create(command.Name);

        _tenantCommandRepository.Insert(tenant);
        await _tenantCommandRepository.CommitAsync();

        return Ok(tenant.Id);
    }
}
