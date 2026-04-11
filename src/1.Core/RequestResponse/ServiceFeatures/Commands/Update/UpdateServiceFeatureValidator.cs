using FluentValidation;
using Master.Data.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Master.Data.Core.RequestResponse.ServiceFeatures.Commands.Update;

public sealed class UpdateServiceFeatureValidator : AbstractValidator<UpdateServiceFeatureCommand>
{
    public UpdateServiceFeatureValidator(ITranslator translator)
    {
        #region Description
        When(c => !string.IsNullOrEmpty(c.Description), () =>
        {
            RuleFor(command => command.Description)
            .Cascade(CascadeMode.Stop)
            .Must(displayTitle => displayTitle.Length >= ProjectConsts.DESCRIPTION_MIN_LENGTH && displayTitle.Length <= ProjectConsts.DESCRIPTION_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.DESCRIPTION,
                                    ProjectConsts.DESCRIPTION_MIN_LENGTH.ToString(),
                                    ProjectConsts.DESCRIPTION_MAX_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        });
        #endregion
    }
}