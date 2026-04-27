using Master.Data.Core.Contracts.ServiceFeatures.Commands;
using Master.Data.Core.Domain.ServiceFeatures.Entities;
using Master.Data.Core.Domain.ServiceFeatures.Parameters;
using Master.Data.Core.RequestResponse.ServiceFeatures.Commands.Create;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.ServiceFeatures.Commands.Create;

public sealed class CreateServiceFeatureHandler : CommandHandler<CreateServiceFeatureCommand, long>
{
    private readonly IServiceFeatureCommandRepository _serviceFeatureCommandRepository;

    public CreateServiceFeatureHandler(ZaminServices zaminServices,
                                       IServiceFeatureCommandRepository serviceFeatureCommandRepository)
        : base(zaminServices)
    {
        _serviceFeatureCommandRepository = serviceFeatureCommandRepository;
    }

    public override async Task<CommandResult<long>> Handle(CreateServiceFeatureCommand command)
    {
        var isDuplicateServiceFeature = await _serviceFeatureCommandRepository
            .ExistsAsync(e => e.Key == command.Key);

        if (isDuplicateServiceFeature)
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.SERVICE_FEATURE]);

        var serviceFeature = ServiceFeature.Create(command.ToParemeter());

        await _serviceFeatureCommandRepository.InsertAsync(serviceFeature);

        await _serviceFeatureCommandRepository.CommitAsync();

        return Ok(serviceFeature.Id);
    }
}