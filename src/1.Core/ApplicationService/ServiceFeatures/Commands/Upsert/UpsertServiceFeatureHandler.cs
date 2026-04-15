using Master.Data.Core.Contracts.ServiceFeatures.Commands;
using Master.Data.Core.Domain.ServiceFeatures.Entities;
using Master.Data.Core.Domain.ServiceFeatures.Parameters;
using Master.Data.Core.RequestResponse.ServiceFeatures.Commands.Upsert;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.ServiceFeatures.Commands.Upsert;

public sealed class UpsertServiceFeatureHandler : CommandHandler<UpsertServiceFeatureCommand>
{
    private readonly IServiceFeatureCommandRepository _serviceFeatureCommandRepository;

    public UpsertServiceFeatureHandler(ZaminServices zaminServices,
                                       IServiceFeatureCommandRepository serviceFeatureCommandRepository)
        : base(zaminServices)
    {
        _serviceFeatureCommandRepository = serviceFeatureCommandRepository;
    }

    public override async Task<CommandResult> Handle(UpsertServiceFeatureCommand command)
    {
        var serviceFeatures = await _serviceFeatureCommandRepository.GetByKeyAsync(command.Key);

        var tenantIds = command.TenantIds.ToHashSet();
        var dict = serviceFeatures.ToDictionary(x => x.TenantId);

        foreach (var tenantId in tenantIds)
        {
            if (dict.TryGetValue(tenantId, out var existing))
            {
                existing.Active();
            }
            else
            {
                var serviceFeature = ServiceFeature.Create(new CreateServiceFeatureWithTenantIdParameter(tenantId, command.Key));

                await _serviceFeatureCommandRepository.InsertAsync(serviceFeature);
            }
        }

        foreach (var feature in serviceFeatures)
        {
            if (!tenantIds.Contains(feature.TenantId))
            {
                feature.Deactive();
            }
        }

        await _serviceFeatureCommandRepository.CommitAsync();


        return Ok();


    }
}



