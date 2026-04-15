using FluentValidation;
using Master.Data.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Master.Data.Core.RequestResponse.ServiceFeatures.Commands.Upsert;

public sealed class UpsertServiceFeatureValidator : AbstractValidator<UpsertServiceFeatureCommand>
{
    public UpsertServiceFeatureValidator(ITranslator translator)
    {
        #region Key
        RuleFor(command => command.Key)
                 .Cascade(CascadeMode.Stop)
                 .IsInEnum()
                 .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_NOT_VALID],
                                            translator[ProjectTranslation.SERVICE_FEATURE_CATEGORY]))
                 .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_IS_NOT_VALID);
        #endregion



        #region TenantIds
        RuleForEach(c => c.TenantIds)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.TENANT_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .GreaterThanOrEqualTo(ProjectConsts.ID_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_DATE_GREATER_THAN_OR_EQUAL,
                                    ProjectTranslation.TENANT_ID,
                                    ProjectConsts.ID_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_OR_EQUAL_THAN);
        #endregion
    }
}
