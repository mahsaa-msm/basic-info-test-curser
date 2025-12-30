using FluentValidation;
using Master.Data.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Master.Data.Core.RequestResponse.TravelPassengerCountTypes.Create;

public class CreateTravelPassengerCountTypeValidator : AbstractValidator<CreateTravelPassengerCountTypeCommand>
{
    public CreateTravelPassengerCountTypeValidator(ITranslator translator)
    {
        RuleFor(x => x.Title)
           .Cascade(CascadeMode.Stop)
           .NotEmpty()
           .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.TRAVEL_PASSENGER_COUNT_TYPE])
           .Length(ProjectConsts.TITLE_MIN_LENGTH, ProjectConsts.TITLE_MAX_LENGTH)
           .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN, ProjectTranslation.TRAVEL_PASSENGER_COUNT_TYPE],
             ProjectTranslation.TRAVEL_PASSENGER_COUNT_TYPE,
             ProjectConsts.TITLE_MIN_LENGTH.ToString(), ProjectConsts.TITLE_MIN_LENGTH.ToString()));

        RuleFor(x => x.CoreId)
           .Cascade(CascadeMode.Stop)
           .NotEmpty()
           .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED], ProjectTranslation.ID))
           .GreaterThanOrEqualTo(ProjectConsts.ID_MIN_VALUE)
           .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN], ProjectTranslation.ID, ProjectConsts.ID_MIN_VALUE.ToString()));
    }
}
