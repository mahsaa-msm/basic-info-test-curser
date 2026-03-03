using FluentValidation;
using Master.Data.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Master.Data.Core.RequestResponse.InsuranceTypes.Commands.Update;

public sealed class UpdateInsuranceTypeValidator : AbstractValidator<UpdateInsuranceTypeCommand>
{
    public UpdateInsuranceTypeValidator(ITranslator translator)
    {
        #region InsuranceTypeId
        RuleFor(c => c.InsuranceTypeId)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.INSURANCE_TYPE_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .GreaterThan(ProjectConsts.ID_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_VALUE_GRATER_THAN,
                                    ProjectTranslation.INSURANCE_TYPE_ID,
                                    ProjectConsts.ID_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_THAN);
        #endregion

        #region Title
        RuleFor(command => command.Title)
             .Cascade(CascadeMode.Stop)
             .NotEmpty()
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.NAME])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

             .Must(title => title.Length >= ProjectConsts.TITLE_MIN_LENGTH && title.Length <= ProjectConsts.NAME_MAX_LENGTH)
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                     ProjectTranslation.TITLE,
                                     ProjectConsts.TITLE_MIN_LENGTH.ToString(),
                                     ProjectConsts.TITLE_MAX_LENGTH.ToString()])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region DisplayTitle
        RuleFor(command => command.DisplayTitle)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.DISPLAY_TITLE])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(displayTitle => displayTitle.Length >= ProjectConsts.TITLE_MIN_LENGTH && displayTitle.Length <= ProjectConsts.TITLE_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.DISPLAY_TITLE,
                                    ProjectConsts.TITLE_MIN_LENGTH.ToString(),
                                    ProjectConsts.TITLE_MAX_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region Code
        RuleFor(command => command.Code)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.CODE])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(code => code.Length >= ProjectConsts.CODE_MIN_LENGTH && code.Length <= ProjectConsts.CODE_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.CODE,
                                    ProjectConsts.CODE_MIN_LENGTH.ToString(),
                                    ProjectConsts.CODE_MAX_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region ServiceFeatureCategory

        When(c => c.ServiceFeatureCategory != null, () =>
        {
            RuleFor(command => command.ServiceFeatureCategory)
           .IsInEnum()
            .WithMessage(translator[ProjectValidationError.INVALID_DATA,
                                    ProjectTranslation.SERVICE_FEATURE_CATEGORY])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_IS_NOT_VALID);
        });

        #endregion

        #region Priority
        RuleFor(command => command.Priority)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.PRIORITY])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(prepaymentPercentage => prepaymentPercentage > ProjectConsts.POSITIVE_NUMBER_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_VALUE_GRATER_THAN,
                                    ProjectTranslation.PRIORITY,
                                    ProjectConsts.POSITIVE_NUMBER_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_THAN);
        #endregion
    }
}