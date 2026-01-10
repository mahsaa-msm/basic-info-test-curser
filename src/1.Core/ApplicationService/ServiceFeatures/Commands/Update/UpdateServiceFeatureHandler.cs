using Master.Data.Core.Contracts.ServiceFeatures.Commands;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Domain.ServiceFeatures.Entities;
using Master.Data.Core.Domain.ServiceFeatures.Parameters;
using Master.Data.Core.RequestResponse.ServiceFeatures.Commands.Update;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.ServiceFeatures.Commands.Update;

public sealed class UpdateServiceFeatureHandler : CommandHandler<UpdateServiceFeatureCommand>
{
    private readonly IServiceFeatureCommandRepository _serviceFeatureCommandRepository;

    public UpdateServiceFeatureHandler(ZaminServices zaminServices,
                                       IServiceFeatureCommandRepository serviceFeatureCommandRepository)
        : base(zaminServices)
    {
        _serviceFeatureCommandRepository = serviceFeatureCommandRepository;
    }

    public override async Task<CommandResult> Handle(UpdateServiceFeatureCommand command)
    {
        ServiceFeature serviceFeature = await _serviceFeatureCommandRepository.GetAsync(command.ServiceFeatureId);

        EntityGuard.ThrowIfNullWithLongId(serviceFeature, ProjectTranslation.SERVICE_FEATURE);

        serviceFeature.Update(new UpdateServiceFeatureParameter(command.ServiceName,
                                                                command.FeatureName,
                                                                command.Description));

        await _serviceFeatureCommandRepository.CommitAsync();

        return Ok();
    }
}