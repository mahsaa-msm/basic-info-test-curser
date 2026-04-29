using FluentValidation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.Update;

public sealed class UpdateVehicleColorValidator : AbstractValidator<UpdateVehicleColorCommand>
{
    public UpdateVehicleColorValidator(ITranslator translator)
    {
        #region VehicleColorId
        RuleFor(c => c.VehicleColorId)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.VEHICLE_COLOR_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .GreaterThan(ProjectConsts.ID_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_VALUE_GRATER_THAN,
                                    ProjectTranslation.VEHICLE_COLOR_ID,
                                    ProjectConsts.ID_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_THAN);
        #endregion

        #region Title
        RuleFor(command => command.Title)
             .Cascade(CascadeMode.Stop)
             .NotEmpty()
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.VEHICLE_COLOR])
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

        #region ColorHash
        RuleFor(command => command.ColorHash)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.COLOR_HASH])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(ColorHash => ColorHash.Length >= ProjectConsts.COLOR_HASH_MIN_LENGTH && ColorHash.Length <= ProjectConsts.COLOR_HASH_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.COLOR_HASH,
                                    ProjectConsts.COLOR_HASH_MIN_LENGTH.ToString(),
                                    ProjectConsts.COLOR_HASH_MAX_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
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



