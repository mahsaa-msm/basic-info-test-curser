using FluentValidation;
using Master.Data.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Master.Data.Core.RequestResponse.Tenants.Commands.ChangeActivation;

public sealed class ChangeTenantsActivationValidator : AbstractValidator<ChangeTenantsActivationCommand>
{
    public ChangeTenantsActivationValidator(ITranslator translator)
    {
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