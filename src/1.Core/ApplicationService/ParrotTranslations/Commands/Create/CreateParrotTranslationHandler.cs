using Vehicle.Insurance.Core.Contracts.ParrotTranslations.Commands;
using Vehicle.Insurance.Core.Domain.ParrotTranslations.Entities;
using Vehicle.Insurance.Core.Domain.ParrotTranslations.Parameters;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Commands.Create;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.ParrotTranslations.Commands.Create;

public sealed class CreateParrotTranslationHandler : CommandHandler<CreateParrotTranslationCommand>
{
    private readonly IParrotTranslationCommandRepository _parrotTranslationCommandRepository;

    public CreateParrotTranslationHandler(ZaminServices zaminServices,
                                          IParrotTranslationCommandRepository parrotTranslationCommandRepository)
        : base(zaminServices)
    {
        _parrotTranslationCommandRepository = parrotTranslationCommandRepository;
    }

    public async override Task<CommandResult> Handle(CreateParrotTranslationCommand command)
    {
        if (await _parrotTranslationCommandRepository.ExistsAsync(e => e.Key == command.Key &&
                                                                       e.Culture == command.Culture))
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                  ProjectTranslation.PARROT_TRANSLATION);

        var parrotTranslation = ParrotTranslation.Create(_zaminServices.MapperFacade
            .Map<CreateParrotTranslationCommand, CreateParrotTranslationParameter>(command));

        _parrotTranslationCommandRepository.Insert(parrotTranslation);
        await _parrotTranslationCommandRepository.CommitAsync();

        return await OkAsync();
    }
}

