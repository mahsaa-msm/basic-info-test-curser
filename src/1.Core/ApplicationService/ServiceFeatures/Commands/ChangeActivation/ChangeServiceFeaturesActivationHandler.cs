using Vehicle.Insurance.Core.Contracts.ServiceFeatures.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.ServiceFeatures.Entities;
using Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Commands.ChangeActivation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.ServiceFeatures.Commands.ChangeActivation;

public sealed class ChangeServiceFeaturesActivationHandler : CommandHandler<ChangeServiceFeaturesActivationCommand>
{
    private readonly IServiceFeatureCommandRepository _serviceFeatureCommandRepository;
    private static readonly Dictionary<bool, Action<List<ServiceFeature>>> _actions = new()
    {
        [true] = c => c.ForEach(c => c.Active()),
        [false] = c => c.ForEach(c => c.Deactive())
    };

    public ChangeServiceFeaturesActivationHandler(ZaminServices zaminServices,
                                                  IServiceFeatureCommandRepository serviceFeatureCommandRepository)
        : base(zaminServices)
    {
        _serviceFeatureCommandRepository = serviceFeatureCommandRepository;
    }

    public override async Task<CommandResult> Handle(ChangeServiceFeaturesActivationCommand command)
    {
        List<ServiceFeature> serviceFeatures = await _serviceFeatureCommandRepository.GetByIds(command.ServiceFeaturesId);

        EntityGuard.ThrowIfListIsEmptyWithLongId(serviceFeatures, ProjectTranslation.SERVICE_FEATURE);

        _actions[command.IsActive](serviceFeatures);
        await _serviceFeatureCommandRepository.CommitAsync();

        return Ok();

    }
}

