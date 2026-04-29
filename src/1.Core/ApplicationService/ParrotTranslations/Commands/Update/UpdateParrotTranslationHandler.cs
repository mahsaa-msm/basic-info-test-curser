using Vehicle.Insurance.Core.Contracts.ParrotTranslations.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.ParrotTranslations.Parameters;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Commands.Update;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.ParrotTranslations.Commands.Update;

public sealed class UpdateParrotTranslationHandler : CommandHandler<UpdateParrotTranslationCommand>
{
    private readonly IParrotTranslationCommandRepository _parrotTranslationCommandRepository;

    public UpdateParrotTranslationHandler(ZaminServices zaminServices,
                                          IParrotTranslationCommandRepository parrotTranslationCommandRepository)
        : base(zaminServices)
    {
        _parrotTranslationCommandRepository = parrotTranslationCommandRepository;
    }

    public async override Task<CommandResult> Handle(UpdateParrotTranslationCommand command)
    {
        var parrotTranslation = await _parrotTranslationCommandRepository.GetAsync(command.Id);

        EntityGuard.ThrowIfNullWithLongId(parrotTranslation, ProjectTranslation.PARROT_TRANSLATION);

        if (await _parrotTranslationCommandRepository.ExistsAsync(e => e.Key == command.Key &&
                                                                       e.Culture == command.Culture &&
                                                                       e.Id != command.Id))
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                  ProjectTranslation.PARROT_TRANSLATION);

        parrotTranslation.Update(_zaminServices.MapperFacade
            .Map<UpdateParrotTranslationCommand, UpdateParrotTranslationParameter>(command));

        await _parrotTranslationCommandRepository.CommitAsync();

        return await OkAsync();
    }
}

