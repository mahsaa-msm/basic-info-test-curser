using FluentValidation;
using Master.Data.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Master.Data.Core.RequestResponse.Cities.Commands.Delete;

public sealed class DeleteCityValidator : AbstractValidator<DeleteCityCommand>
{
    public DeleteCityValidator(ITranslator translator)
    {
        #region CityId
        RuleFor(c => c.CityId)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.CITY_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .GreaterThan(ProjectConsts.ID_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_VALUE_GRATER_THAN,
                                    ProjectTranslation.CITY_ID,
                                    ProjectConsts.ID_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_THAN);
        #endregion
    }
}