using FluentValidation;
using Master.Data.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Master.Data.Core.RequestResponse.InsuranceUnits.Queries.GetAllInArea;
public sealed class GetAllInsuranceUnitsInAreaValidator : AbstractValidator<GetAllInsuranceUnitsInAreaQuery>
{
    public GetAllInsuranceUnitsInAreaValidator(ITranslator translator)
    {
        RuleFor(x => x)
            .Must(HaveAtLeastOneFilter)
            .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_AT_LEAST_ONE_OF_REQUIRED],
                                       translator[ProjectTranslation.FILTER]));

        #region Area
        When(x => x.Area != null, () =>
        {
            RuleFor(x => x.Area)
                .Must(area => area!.IsValid())
                .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_INVALID_GEOGRAPHIC_AREA]);

            RuleFor(x => x.Area!.MinLatitude)
                .InclusiveBetween(ProjectConsts.LATITUDE_MIN_VALUE, ProjectConsts.LATITUDE_MAX_VALUE)
                .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_RANGE],
                                          translator[ProjectTranslation.LATITUDE],
                                          ProjectConsts.LATITUDE_MIN_VALUE,
                                          ProjectConsts.LATITUDE_MAX_VALUE))
                .LessThan(x => x.Area!.MaxLatitude)
                .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_MIN_LATITUDE_LESS_THAN_MAX]);

            RuleFor(x => x.Area!.MinLongitude)
                .InclusiveBetween(ProjectConsts.LONGITUDE_MIN_VALUE, ProjectConsts.LONGITUDE_MAX_VALUE)
                .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_RANGE],
                                          translator[ProjectTranslation.LONGITUDE],
                                          ProjectConsts.LONGITUDE_MIN_VALUE,
                                          ProjectConsts.LONGITUDE_MAX_VALUE))
                .LessThan(x => x.Area!.MaxLongitude)
                .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_MIN_LONGITUDE_LESS_THAN_MAX]);

            RuleFor(x => x.Area!.MaxLatitude)
                .InclusiveBetween(ProjectConsts.LATITUDE_MIN_VALUE, ProjectConsts.LATITUDE_MAX_VALUE)
                .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_RANGE],
                                          translator[ProjectTranslation.LATITUDE],
                                          ProjectConsts.LATITUDE_MIN_VALUE,
                                          ProjectConsts.LATITUDE_MAX_VALUE));

            RuleFor(x => x.Area!.MaxLongitude)
                .InclusiveBetween(ProjectConsts.LONGITUDE_MIN_VALUE, ProjectConsts.LONGITUDE_MAX_VALUE)
                .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_RANGE],
                                          translator[ProjectTranslation.LONGITUDE],
                                          ProjectConsts.LONGITUDE_MIN_VALUE,
                                          ProjectConsts.LONGITUDE_MAX_VALUE));

            RuleFor(x => x.Area)
                .Must(area => Math.Abs(area!.MaxLatitude - area.MinLatitude) <= ProjectConsts.MAX_LATITUDE_DIFFERENCE)
                .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_MAX_LATITUDE_DIFFERENCE],
                                          ProjectConsts.MAX_LATITUDE_DIFFERENCE))
                .Must(area => Math.Abs(area!.MaxLongitude - area.MinLongitude) <= ProjectConsts.MAX_LONGITUDE_DIFFERENCE)
                .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_MAX_LONGITUDE_DIFFERENCE],
                                          ProjectConsts.MAX_LONGITUDE_DIFFERENCE));
        });
        #endregion

        // اعتبارسنجی SearchInput
        When(x => !string.IsNullOrEmpty(x.SearchInput), () =>
        {
            RuleFor(x => x.SearchInput)
                .MaximumLength(ProjectConsts.SEARCH_INPUT_MAX_LENGTH)
                .WithMessage(string.Format(translator[ProjectValidationError.VALIDATION_ERROR_STRING_MAX_LENGTH],
                                          translator[ProjectTranslation.SEARCH_INPUT],
                                          ProjectConsts.SEARCH_INPUT_MAX_LENGTH))
                .Matches(ProjectConsts.SEARCH_INPUT_PATTERN)
                .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_INVALID_SEARCH_INPUT_PATTERN]);
        });

        When(x => !string.IsNullOrEmpty(x.ProvinceCoreId), () =>
        {
            RuleFor(x => x.ProvinceCoreId)
            .Must(coreId => coreId!.Length >= ProjectConsts.CORE_ID_MIN_LENGTH && coreId.Length <= ProjectConsts.CORE_ID_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.PROVINCE_ID,
                                    ProjectConsts.CORE_ID_MAX_LENGTH.ToString(),
                                    ProjectConsts.CORE_ID_MIN_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        });

        When(x => !string.IsNullOrEmpty(x.CityCoreId), () =>
        {
            RuleFor(x => x.CityCoreId)
            .Must(coreId => coreId!.Length >= ProjectConsts.CORE_ID_MIN_LENGTH && coreId.Length <= ProjectConsts.CORE_ID_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.CITY_ID,
                                    ProjectConsts.CORE_ID_MAX_LENGTH.ToString(),
                                    ProjectConsts.CORE_ID_MIN_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        });
    }
    private bool HaveAtLeastOneFilter(GetAllInsuranceUnitsInAreaQuery query)
    {
        return query.Area != null ||
               !string.IsNullOrEmpty(query.ProvinceCoreId) ||
               !string.IsNullOrEmpty(query.CityCoreId) ||
               query.Type.HasValue ||
               !string.IsNullOrEmpty(query.SearchInput);
    }
}
