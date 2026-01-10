using Master.Data.Core.Contracts.ServiceFeatures.Commands;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Domain.ServiceFeatures.Entities;
using Master.Data.Core.RequestResponse.ServiceFeatures.Commands.ChangeActivation;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.ServiceFeatures.Commands.ChangeActivation;

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
