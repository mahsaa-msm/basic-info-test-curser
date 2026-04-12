using FluentValidation;
using Master.Data.Core.Resources;
using System.Text.RegularExpressions;
using Zamin.Extensions.Translations.Abstractions;

namespace Master.Data.Core.RequestResponse.Tenants.Commands.Create;

public sealed class CreateTenantValidator : AbstractValidator<CreateTenantCommand>
{
    public CreateTenantValidator(ITranslator translator)
    {
        #region Name
        RuleFor(command => command.Name)
             .Cascade(CascadeMode.Stop)
             .NotEmpty()
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.NAME])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

             .Must(name => name.Length >= ProjectConsts.NAME_MIN_LENGTH && name.Length <= ProjectConsts.NAME_MAX_LENGTH)
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                     ProjectTranslation.NAME,
                                     ProjectConsts.NAME_MIN_LENGTH.ToString(),
                                     ProjectConsts.NAME_MAX_LENGTH.ToString()])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region slug
        RuleFor(command => command.Slug)
           .Cascade(CascadeMode.Stop)
           .NotEmpty()
           .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.TENANT_SLUG])
           .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

           .Must(slug => slug?.Trim().ToLowerInvariant().Length >= ProjectConsts.TENANT_SLUG_MIN_LENGTH &&
                 slug?.Trim().ToLowerInvariant().Length <= ProjectConsts.TENANT_SLUG_MAX_LENGTH)
           .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                   ProjectTranslation.TENANT_SLUG,
                                   ProjectConsts.TENANT_SLUG_MIN_LENGTH.ToString(),
                                   ProjectConsts.TENANT_SLUG_MAX_LENGTH.ToString()])
           .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH)

           .Must(slug => Regex.IsMatch(slug?.Trim().ToLowerInvariant() ?? string.Empty, ProjectConsts.TENANT_SLUG_VALIDATION_PATTERN))
           .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_NOT_VALID, ProjectTranslation.TENANT_SLUG])
           .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_IS_NOT_VALID);
        #endregion
    }
}
