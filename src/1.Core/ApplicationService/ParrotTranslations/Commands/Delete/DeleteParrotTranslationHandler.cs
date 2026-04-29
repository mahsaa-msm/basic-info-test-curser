using Vehicle.Insurance.Core.Contracts.ParrotTranslations.Commands;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Commands.Delete;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.ParrotTranslations.Commands.Delete;

public sealed class DeleteParrotTranslationHandler : CommandHandler<DeleteParrotTranslationCommand>
{
    private readonly IParrotTranslationCommandRepository _parrotTranslationCommandRepository;

    public DeleteParrotTranslationHandler(ZaminServices zaminServices,
                                          IParrotTranslationCommandRepository parrotTranslationCommandRepository)
        : base(zaminServices)
    {
        _parrotTranslationCommandRepository = parrotTranslationCommandRepository;
    }

    public async override Task<CommandResult> Handle(DeleteParrotTranslationCommand command)
    {
        if (!await _parrotTranslationCommandRepository.ExistsAsync(e => e.Id == command.Id))
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.PARROT_TRANSLATION);

        _parrotTranslationCommandRepository.Delete(command.Id);
        await _parrotTranslationCommandRepository.CommitAsync();

        return await OkAsync();
    }
}

