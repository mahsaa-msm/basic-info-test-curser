using FluentValidation;
using Master.Data.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Master.Data.Core.RequestResponse.Provinces.Commands.Update;

public sealed class UpdateProvinceValidator : AbstractValidator<UpdateProvinceCommand>
{
    public UpdateProvinceValidator(ITranslator translator)
    {
        #region ProvinceId
        RuleFor(c => c.ProvinceId)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.PROVINCE_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .GreaterThan(ProjectConsts.ID_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_VALUE_GRATER_THAN,
                                    ProjectTranslation.PROVINCE_ID,
                                    ProjectConsts.ID_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_THAN);
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

        #region Title
        When(command => !string.IsNullOrEmpty(command.Title), () =>
        {
            RuleFor(command => command.Title)
           .Cascade(CascadeMode.Stop)
           .Must(title => title.Length >= ProjectConsts.TITLE_MIN_LENGTH && title.Length <= ProjectConsts.NAME_MAX_LENGTH)
           .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                   ProjectTranslation.TITLE,
                                   ProjectConsts.TITLE_MIN_LENGTH.ToString(),
                                   ProjectConsts.TITLE_MAX_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        });
        #endregion

        #region Code
        When(command => !string.IsNullOrEmpty(command.Code), () =>
        {
            RuleFor(command => command.Code)
             .Cascade(CascadeMode.Stop)
             .Must(code => code.Length >= ProjectConsts.CODE_MIN_LENGTH && code.Length <= ProjectConsts.CODE_MAX_LENGTH)
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                     ProjectTranslation.CODE,
                                     ProjectConsts.CODE_MIN_LENGTH.ToString(),
                                     ProjectConsts.CODE_MAX_LENGTH.ToString()])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        });
        #endregion

        #region CountryCoreId
        RuleFor(command => command.CountryCoreId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.COUNTRY_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(coreId => coreId.Length >= ProjectConsts.CORE_ID_MIN_LENGTH && coreId.Length <= ProjectConsts.CORE_ID_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.COUNTRY_ID,
                                    ProjectConsts.CORE_ID_MAX_LENGTH.ToString(),
                                    ProjectConsts.CORE_ID_MIN_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion
    }
}
