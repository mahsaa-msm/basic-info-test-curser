using FluentValidation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.Delete;

public sealed class DeleteVehicleColorValidator : AbstractValidator<DeleteVehicleColorCommand>
{
    public DeleteVehicleColorValidator(ITranslator translator)
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
    }
}

