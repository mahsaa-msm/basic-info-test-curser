using FluentValidation;
using Vehicle.Insurance.Core.RequestResponse.Common.Extensions;
using Vehicle.Insurance.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.Create;

public sealed class CreateVehicleColorValidator : AbstractValidator<CreateVehicleColorCommand>
{
    public CreateVehicleColorValidator(ITranslator translator)
    {
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
    }
}



