using FluentValidation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.ChangeActivation;

public sealed class ChangeVehicleColorsActivationValidator : AbstractValidator<ChangeVehicleColorsActivationCommand>
{
    public ChangeVehicleColorsActivationValidator(ITranslator translator)
    {
        #region VehicleColorsId
        RuleForEach(c => c.VehicleColorsId)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.VEHICLE_COLOR_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .GreaterThanOrEqualTo(ProjectConsts.ID_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_DATE_GREATER_THAN_OR_EQUAL,
                                    ProjectTranslation.VEHICLE_COLOR_ID,
                                    ProjectConsts.ID_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_OR_EQUAL_THAN);
        #endregion
    }
}

