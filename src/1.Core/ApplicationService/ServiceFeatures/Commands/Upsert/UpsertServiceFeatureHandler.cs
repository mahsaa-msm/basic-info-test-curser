using Master.Data.Core.Contracts.ServiceFeatures.Commands;
using Master.Data.Core.Domain.ServiceFeatures.Entities;
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

        foreach (var tenantId in tenantIds)
        {
            var existing = serviceFeatures.FirstOrDefault(f => f.TenantId == tenantId);

            if (existing is not null)
            {
                SetActiveStatus(existing, command.IsActive);
            }
            else
            {
                var newFeature = ServiceFeature.Create(command.ToParemeter(tenantId));
                SetActiveStatus(newFeature, command.IsActive);
                await _serviceFeatureCommandRepository.InsertAsync(newFeature);
            }
        }

        foreach (var feature in serviceFeatures.Where(f => !tenantIds.Contains(f.TenantId)))
        {
            feature.Deactive();
        }

        await _serviceFeatureCommandRepository.CommitAsync();
        return Ok();
    }

    #region Private methods
    private static void SetActiveStatus(ServiceFeature feature, bool isActive)
    {
        if (isActive)
            feature.Active();
        else
            feature.Deactive();
    }
    #endregion
}



