using FluentValidation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleTips.Commands.Create;

public sealed class CreateVehicleTipValidator : AbstractValidator<CreateVehicleTipCommand>
{
    public CreateVehicleTipValidator(ITranslator translator)
    {
        RuleFor(command => command.Title)
             .Cascade(CascadeMode.Stop)
             .NotEmpty()
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.VEHICLE_TIP])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)
             .Must(title => title.Length >= ProjectConsts.TITLE_MIN_LENGTH && title.Length <= ProjectConsts.NAME_MAX_LENGTH)
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                     ProjectTranslation.TITLE,
                                     ProjectConsts.TITLE_MIN_LENGTH.ToString(),
                                     ProjectConsts.TITLE_MAX_LENGTH.ToString()])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);

        When(c => !string.IsNullOrEmpty(c.DisplayTitle), () =>
        {
            RuleFor(command => command.DisplayTitle)
            .Cascade(CascadeMode.Stop)
            .Must(displayTitle => displayTitle!.Length >= ProjectConsts.TITLE_MIN_LENGTH && displayTitle!.Length <= ProjectConsts.TITLE_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.DISPLAY_TITLE,
                                    ProjectConsts.TITLE_MIN_LENGTH.ToString(),
                                    ProjectConsts.TITLE_MAX_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        });

        AddCoreIdRules(command => command.CoreId, translator);
        AddCoreIdRules(command => command.BrandCoreId, translator);
        AddCoreIdRules(command => command.VehicleTypeCoreId, translator);
        AddCoreIdRules(command => command.VehicleSystemCoreId, translator);
    }

    private void AddCoreIdRules(System.Linq.Expressions.Expression<System.Func<CreateVehicleTipCommand, string>> propertyExpression,
                               ITranslator translator)
    {
        RuleFor(propertyExpression)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.CORE_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)
            .Must(coreId => coreId!.Length >= ProjectConsts.CORE_ID_MIN_LENGTH && coreId.Length <= ProjectConsts.CORE_ID_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.CORE_ID,
                                    ProjectConsts.CORE_ID_MIN_LENGTH.ToString(),
                                    ProjectConsts.CORE_ID_MAX_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
    }
}
