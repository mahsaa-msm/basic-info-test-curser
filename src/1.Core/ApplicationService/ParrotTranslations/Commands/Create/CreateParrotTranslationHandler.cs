using Master.Data.Core.Contracts.ParrotTranslations.Commands;
using Master.Data.Core.Domain.ParrotTranslations.Entities;
using Master.Data.Core.Domain.ParrotTranslations.Parameters;
using Master.Data.Core.RequestResponse.ParrotTranslations.Commands.Create;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.ParrotTranslations.Commands.Create;

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
