using Master.Data.Core.Contracts.ParrotTranslations.Commands;
using Master.Data.Core.RequestResponse.ParrotTranslations.Commands.Delete;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.ParrotTranslations.Commands.Delete;

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
