using FluentValidation;
using Vehicle.Insurance.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Commands.Create;

public sealed class CreateServiceFeatureValidator : AbstractValidator<CreateServiceFeatureCommand>
{
    public CreateServiceFeatureValidator(ITranslator translator)
    {
        #region Key
        RuleFor(command => command.Key)
             .Cascade(CascadeMode.Stop)
             .NotEmpty()
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.SERVICE_FEATURE_KEY])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)
             .IsInEnum()
             .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_NOT_VALID],
                                        ProjectTranslation.SERVICE_FEATURE_KEY))
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_IS_NOT_VALID)
             .Must(key => ServiceFeatureCategoryHelper.GetLevel((long)key) >= 2)
             .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_NOT_VALID],
                                        ProjectTranslation.SERVICE_FEATURE_KEY))
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


        #region InsuranceTypeCoreId
        When(c => !string.IsNullOrEmpty(c.InsuranceTypeCoreId), () =>
        {
            RuleFor(command => command.InsuranceTypeCoreId)
            .Must(coreId => coreId.Length >= ProjectConsts.CORE_ID_MIN_LENGTH && coreId.Length <= ProjectConsts.CORE_ID_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.INSURANCE_TYPE,
                                    ProjectConsts.CORE_ID_MAX_LENGTH.ToString(),
                                    ProjectConsts.CORE_ID_MIN_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        });

        #endregion
    }
}
