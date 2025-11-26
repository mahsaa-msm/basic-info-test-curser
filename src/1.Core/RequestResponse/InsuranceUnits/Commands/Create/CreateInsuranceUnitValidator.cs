using FluentValidation;
using Master.Data.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Master.Data.Core.RequestResponse.InsuranceUnits.Commands.Create;
public sealed class CreateInsuranceUnitValidator : AbstractValidator<CreateInsuranceUnitCommand>
{
    public CreateInsuranceUnitValidator(ITranslator translator)
    {
        #region Name
        RuleFor(command => command.Name)
             .Cascade(CascadeMode.Stop)
             .NotEmpty()
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.NAME])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

             .Must(title => title.Length >= ProjectConsts.NAME_MIN_LENGTH && title.Length <= ProjectConsts.NAME_MAX_LENGTH)
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                     ProjectTranslation.NAME,
                                     ProjectConsts.NAME_MIN_LENGTH.ToString(),
                                     ProjectConsts.NAME_MAX_LENGTH.ToString()])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region Title
        RuleFor(command => command.Title)
             .Cascade(CascadeMode.Stop)
             .NotEmpty()
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.TITLE])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

             .Must(title => title.Length >= ProjectConsts.TITLE_MIN_LENGTH && title.Length <= ProjectConsts.TITLE_MAX_LENGTH)
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                     ProjectTranslation.TITLE,
                                     ProjectConsts.TITLE_MIN_LENGTH.ToString(),
                                     ProjectConsts.TITLE_MAX_LENGTH.ToString()])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region DisplayTitle

        When(c => !string.IsNullOrEmpty(c.DisplayTitle), () =>
        {
            RuleFor(command => command.DisplayTitle)
            .Cascade(CascadeMode.Stop)
            .Must(displayTitle => displayTitle.Length >= ProjectConsts.TITLE_MIN_LENGTH && displayTitle.Length <= ProjectConsts.TITLE_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.DISPLAY_TITLE,
                                    ProjectConsts.TITLE_MIN_LENGTH.ToString(),
                                    ProjectConsts.TITLE_MAX_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        });

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

        #region CoreId
        RuleFor(command => command.CoreId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.CORE_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(coreId => coreId.Length >= ProjectConsts.CORE_ID_MIN_LENGTH && coreId.Length <= ProjectConsts.CORE_ID_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.CORE_ID,
                                    ProjectConsts.CORE_ID_MAX_LENGTH.ToString(),
                                    ProjectConsts.CORE_ID_MIN_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region CityCoreId
        RuleFor(command => command.CityCoreId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.CITY_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(coreId => coreId.Length >= ProjectConsts.CORE_ID_MIN_LENGTH && coreId.Length <= ProjectConsts.CORE_ID_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.CITY_ID,
                                    ProjectConsts.CORE_ID_MAX_LENGTH.ToString(),
                                    ProjectConsts.CORE_ID_MIN_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region Location
        RuleFor(x => x)
            .Must(x => (x.Latitude.HasValue && x.Longitude.HasValue) ||
                      (!x.Latitude.HasValue && !x.Longitude.HasValue))
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_BOTH_OR_ANY_LATITUDE_LONGITUDE_MUST_EXIST]);

        RuleFor(x => x.Latitude)
            .InclusiveBetween(ProjectConsts.LATITUDE_MIN_VALUE, ProjectConsts.LATITUDE_MAX_VALUE)
            .When(x => x.Latitude.HasValue)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_NUMBER_BETWEEN,
                                    ProjectTranslation.LATITUDE,
                                    ProjectConsts.LATITUDE_MAX_VALUE.ToString(),
                                    ProjectConsts.LATITUDE_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_BETWEEN);

        RuleFor(x => x.Longitude)
            .InclusiveBetween(ProjectConsts.LONGITUDE_MIN_VALUE, ProjectConsts.LONGITUDE_MAX_VALUE)
            .When(x => x.Longitude.HasValue)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_NUMBER_BETWEEN,
                                    ProjectTranslation.LONGITUDE,
                                    ProjectConsts.LONGITUDE_MAX_VALUE.ToString(),
                                    ProjectConsts.LONGITUDE_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_BETWEEN);
        #endregion

        #region Type
        RuleFor(command => command.Type)
            .NotNull()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.INSURANCE_UNIT_TYPE])
                        .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .IsInEnum()
            .WithMessage(translator[ProjectValidationError.INVALID_DATA, ProjectTranslation.INSURANCE_UNIT_TYPE])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_IS_NOT_VALID);
        #endregion

        #region State
        RuleFor(command => command.State)
            .NotNull()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.INSURANCE_UNIT_STATE])
                        .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .IsInEnum()
            .WithMessage(translator[ProjectValidationError.INVALID_DATA, ProjectTranslation.INSURANCE_UNIT_STATE])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_IS_NOT_VALID);
        #endregion
    }
}