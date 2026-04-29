using Vehicle.Insurance.Core.Contracts.ServiceFeatures.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.ServiceFeatures.Entities;
using Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Commands.Update;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.ServiceFeatures.Commands.Update;

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

        serviceFeature.Update(command.ToParemeter());

        await _serviceFeatureCommandRepository.CommitAsync();

        return Ok();
    }
}
