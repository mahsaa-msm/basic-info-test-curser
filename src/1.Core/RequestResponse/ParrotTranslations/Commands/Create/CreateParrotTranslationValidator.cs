using FluentValidation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Commands.Create;

public sealed class CreateParrotTranslationValidator : AbstractValidator<CreateParrotTranslationCommand>
{
    public CreateParrotTranslationValidator(ITranslator translator)
    {
        RuleFor(x => x.Key)
        .Cascade(CascadeMode.Stop)
        .NotEmpty()
        .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED,
            ProjectTranslation.TRANSLATION_KEY])
        .Length(ProjectConsts.TRANSLATION_KEY_MIN_LENGTH, ProjectConsts.TRANSLATION_KEY_MAX_LENGTH)
        .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN],
            translator[ProjectTranslation.TRANSLATION_KEY],
            ProjectConsts.TRANSLATION_KEY_MIN_LENGTH.ToString(),
            ProjectConsts.TRANSLATION_KEY_MAX_LENGTH.ToString()));

        RuleFor(x => x.Value)
        .Cascade(CascadeMode.Stop)
        .NotEmpty()
        .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED,
            ProjectTranslation.TRANSLATION_VALUE])
        .Length(ProjectConsts.TRANSLATION_VALUE_MIN_LENGTH, ProjectConsts.TRANSLATION_VALUE_MAX_LENGTH)
        .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN],
            translator[ProjectTranslation.TRANSLATION_VALUE],
            ProjectConsts.TRANSLATION_VALUE_MIN_LENGTH.ToString(),
            ProjectConsts.TRANSLATION_VALUE_MAX_LENGTH.ToString()));

        RuleFor(x => x.Culture)
        .Cascade(CascadeMode.Stop)
        .Length(ProjectConsts.TRANSLATION_CULTURE_LENGTH)
        .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_MUST_EQUAL],
            translator[ProjectTranslation.TRANSLATION_CULTURE],
            ProjectConsts.TRANSLATION_CULTURE_LENGTH.ToString()));
    }
}

