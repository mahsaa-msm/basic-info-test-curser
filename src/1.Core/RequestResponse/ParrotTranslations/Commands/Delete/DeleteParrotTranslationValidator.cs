using FluentValidation;
using Master.Data.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Master.Data.Core.RequestResponse.ParrotTranslations.Commands.Delete;

public sealed class DeleteParrotTranslationValidator : AbstractValidator<DeleteParrotTranslationCommand>
{
    public DeleteParrotTranslationValidator(ITranslator translator)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED,
                ProjectTranslation.ID])
            .GreaterThanOrEqualTo(ProjectConsts.ID_MIN_VALUE)
            .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN],
                translator[ProjectTranslation.ID],
                ProjectConsts.ID_MIN_VALUE.ToString()));
    }
}
