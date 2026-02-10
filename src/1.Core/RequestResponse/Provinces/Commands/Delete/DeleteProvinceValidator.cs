using FluentValidation;
using Master.Data.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Master.Data.Core.RequestResponse.Provinces.Commands.Delete;

public sealed class DeleteProvinceValidator : AbstractValidator<DeleteProvinceCommand>
{
    public DeleteProvinceValidator(ITranslator translator)
    {
        #region ProvinceId
        RuleFor(c => c.ProvinceId)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.PROVINCE_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .GreaterThan(ProjectConsts.ID_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_VALUE_GRATER_THAN,
                                    ProjectTranslation.PROVINCE_ID,
                                    ProjectConsts.ID_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_THAN);
        #endregion
    }
}