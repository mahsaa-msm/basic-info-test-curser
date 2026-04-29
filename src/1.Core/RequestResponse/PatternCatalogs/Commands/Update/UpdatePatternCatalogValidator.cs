using FluentValidation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Commands.Update;

public sealed class UpdatePatternCatalogValidator : AbstractValidator<UpdatePatternCatalogCommand>
{
    public UpdatePatternCatalogValidator(ITranslator translator)
    {
        #region PatternCatalogId
        RuleFor(c => c.PatternCatalogId)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.PATTERN_CATALOG_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .GreaterThan(ProjectConsts.ID_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_VALUE_GRATER_THAN,
                                    ProjectTranslation.PATTERN_CATALOG_ID,
                                    ProjectConsts.ID_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_THAN);
        #endregion

        #region Pattern
        RuleFor(command => command.Pattern)
             .Cascade(CascadeMode.Stop)
             .NotEmpty()
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.REGEX_EXPRESSION])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

             .MaximumLength(ProjectConsts.PATTERN_MAX_LENGTH)
             .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_STRING_MAX_LENGTH],
                                        ProjectTranslation.REGEX_EXPRESSION,
                                        ProjectConsts.PATTERN_MAX_LENGTH))
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region Type
        RuleFor(command => command.Type)
             .Cascade(CascadeMode.Stop)
             .IsInEnum()
             .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_NOT_VALID],
                                        translator[ProjectTranslation.REGEX_EXPRESSION]))
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_IS_NOT_VALID);
        #endregion

        #region Description
        When(c => !string.IsNullOrEmpty(c.Description), () =>
        {
            RuleFor(command => command.Description)
            .Cascade(CascadeMode.Stop)
            .Must(description => description.Length >= ProjectConsts.DESCRIPTION_MIN_LENGTH && description.Length <= ProjectConsts.DESCRIPTION_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.DESCRIPTION,
                                    ProjectConsts.DESCRIPTION_MIN_LENGTH.ToString(),
                                    ProjectConsts.DESCRIPTION_MAX_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        });
        #endregion
    }
}
