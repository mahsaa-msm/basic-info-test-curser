using Vehicle.Insurance.Core.Contracts.Tenants.Comamnds;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.Tenants.Entities;
using Vehicle.Insurance.Core.Domain.Tenants.ValueObjects;
using Vehicle.Insurance.Core.RequestResponse.Tenants.Commands.Create;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.Tenants.Commands;

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
        var tenantExist = await _tenantCommandRepository.ExistsAsync(c => c.Name == DIPTitle.FromString(command.Name)
                                                                          || c.Slug == TenantSlug.FromString(command.Slug));
        if (tenantExist)
            throw new ApplicationException(string.Format(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE],
                                                         ProjectTranslation.TENANT));

        var tenant = Tenant.Create(command.Name, command.Slug);

        _tenantCommandRepository.Insert(tenant);
        await _tenantCommandRepository.CommitAsync();

        return Ok(tenant.Id);
    }
}

