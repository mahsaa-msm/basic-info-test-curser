using FluentValidation;
using Master.Data.Core.Resources;
using Zamin.Extensions.Translations.Abstractions;

namespace Master.Data.Core.RequestResponse.AgreementObligations.Commands.Update;

public sealed class UpdateAgreementObligationValidator : AbstractValidator<UpdateAgreementObligationCommand>
{
    public UpdateAgreementObligationValidator(ITranslator translator)
    {
        #region Title
        RuleFor(command => command.Title)
             .Cascade(CascadeMode.Stop)
             .NotEmpty()
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.NAME])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

             .Must(title => title.Length >= ProjectConsts.TITLE_MIN_LENGTH && title.Length <= ProjectConsts.NAME_MAX_LENGTH)
             .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                     ProjectTranslation.TITLE,
                                     ProjectConsts.TITLE_MIN_LENGTH.ToString(),
                                     ProjectConsts.TITLE_MAX_LENGTH.ToString()])
             .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region DisplayTitle
        RuleFor(command => command.DisplayTitle)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.DISPLAY_TITLE])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(displayTitle => displayTitle.Length >= ProjectConsts.TITLE_MIN_LENGTH && displayTitle.Length <= ProjectConsts.TITLE_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.DISPLAY_TITLE,
                                    ProjectConsts.TITLE_MIN_LENGTH.ToString(),
                                    ProjectConsts.TITLE_MAX_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region Code
        RuleFor(command => command.Code)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.CODE])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(code => code.Length >= ProjectConsts.CODE_MIN_LENGTH && code.Length <= ProjectConsts.CODE_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.CODE,
                                    ProjectConsts.CODE_MIN_LENGTH.ToString(),
                                    ProjectConsts.CODE_MAX_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region CoreId
        RuleFor(command => command.CoreId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.CORE_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(coreId => coreId.Length >= ProjectConsts.CORE_ID_MIN_LENGTH && coreId.Length <= ProjectConsts.CORE_ID_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.CORE_ID,
                                    ProjectConsts.CORE_ID_MAX_LENGTH.ToString(),
                                    ProjectConsts.CORE_ID_MIN_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region AgreementCoreId
        RuleFor(command => command.AgreementCoreId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.AGREEMENT_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(coreId => coreId.Length >= ProjectConsts.CORE_ID_MIN_LENGTH && coreId.Length <= ProjectConsts.CORE_ID_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.AGREEMENT_ID,
                                    ProjectConsts.CORE_ID_MAX_LENGTH.ToString(),
                                    ProjectConsts.CORE_ID_MIN_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region InsuranceTypeCoreId
        RuleFor(command => command.InsuranceTypeCoreId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.INSURANCE_TYPE_ID])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(coreId => coreId.Length >= ProjectConsts.CORE_ID_MIN_LENGTH && coreId.Length <= ProjectConsts.CORE_ID_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.INSURANCE_TYPE_ID,
                                    ProjectConsts.CORE_ID_MAX_LENGTH.ToString(),
                                    ProjectConsts.CORE_ID_MIN_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region StartDateUtc
        RuleFor(command => command.StartDateUtc)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.START_DATE])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(startDate => startDate > DateTime.MinValue && startDate < DateTime.MaxValue)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_DATE_BETWEEN,
                                    ProjectTranslation.START_DATE,
                                    DateTime.MinValue.ToString(),
                                    DateTime.MaxValue.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_DATE_BETWEEN);
        #endregion

        #region EndDateUtc
        RuleFor(command => command.EndDateUtc)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.END_DATE])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(startDate => startDate > DateTime.MinValue && startDate < DateTime.MaxValue)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_DATE_BETWEEN,
                                    ProjectTranslation.END_DATE,
                                    DateTime.MinValue.ToString(),
                                    DateTime.MaxValue.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_DATE_BETWEEN);
        #endregion

        #region PrepaymentPercentage
        RuleFor(command => command.PrepaymentPercentage)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.PREPAYMENT_PERCENTAGE])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(prepaymentPercentage => prepaymentPercentage > ProjectConsts.PERCENTAGE_MIN_VALUE &&
                                          prepaymentPercentage < ProjectConsts.PERCENTAGE_MAX_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_NUMBER_BETWEEN,
                                    ProjectTranslation.PREPAYMENT_PERCENTAGE,
                                    ProjectConsts.PERCENTAGE_MIN_VALUE.ToString(),
                                    ProjectConsts.PERCENTAGE_MAX_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_BETWEEN);
        #endregion

        #region FirstInstallmentDeadline
        RuleFor(command => command.FirstInstallmentDeadline)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.FIRST_INSTALLMENT_DEADLINE])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .GreaterThanOrEqualTo(ProjectConsts.POSITIVE_NUMBER_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN,
                                    ProjectTranslation.FIRST_INSTALLMENT_DEADLINE,
                                    ProjectConsts.POSITIVE_NUMBER_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_OR_EQUAL_THAN);
        #endregion

        #region InstallmentsCount
        RuleFor(command => command.InstallmentsCount)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.INSTALLMENTS_COUNT])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .GreaterThanOrEqualTo(ProjectConsts.POSITIVE_NUMBER_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN,
                                    ProjectTranslation.INSTALLMENTS_COUNT,
                                    ProjectConsts.POSITIVE_NUMBER_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_OR_EQUAL_THAN);
        #endregion

        #region InstallmentInterval
        RuleFor(command => command.InstallmentInterval)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.INSTALLMENT_INTERVAL])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .GreaterThanOrEqualTo(ProjectConsts.POSITIVE_NUMBER_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN,
                                    ProjectTranslation.INSTALLMENT_INTERVAL,
                                    ProjectConsts.POSITIVE_NUMBER_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_OR_EQUAL_THAN);
        #endregion

        #region AgreementNumber
        RuleFor(command => command.AgreementNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.AGREEMENT_NUMBER])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(coreId => coreId.Length >= ProjectConsts.AGREEMENT_NUMBER_MIN_LENGTH && coreId.Length <= ProjectConsts.AGREEMENT_NUMBER_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.AGREEMENT_NUMBER,
                                    ProjectConsts.AGREEMENT_NUMBER_MIN_LENGTH.ToString(),
                                    ProjectConsts.AGREEMENT_NUMBER_MAX_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region AgreementObligationNumber
        RuleFor(command => command.AgreementObligationNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.AGREEMENT_OBLIGATION_NUMBER])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(coreId => coreId.Length >= ProjectConsts.AGREEMENT_NUMBER_MIN_LENGTH && coreId.Length <= ProjectConsts.AGREEMENT_NUMBER_MAX_LENGTH)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN,
                                    ProjectTranslation.AGREEMENT_OBLIGATION_NUMBER,
                                    ProjectConsts.AGREEMENT_NUMBER_MIN_LENGTH.ToString(),
                                    ProjectConsts.AGREEMENT_NUMBER_MAX_LENGTH.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_STRING_LENGTH);
        #endregion

        #region SalesType
        RuleFor(command => command.SalesType)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.SALES_TYPE])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .IsInEnum()
            .WithMessage(translator[ProjectValidationError.INVALID_DATA,
                                    ProjectTranslation.SALES_TYPE])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_IS_NOT_VALID);
        #endregion

        #region Priority
        RuleFor(command => command.Priority)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_REQUIRED, ProjectTranslation.PRIORITY])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_REQUIRED)

            .Must(prepaymentPercentage => prepaymentPercentage > ProjectConsts.POSITIVE_NUMBER_MIN_VALUE)
            .WithMessage(translator[ProjectValidationError.VALIDATION_ERROR_VALUE_GRATER_THAN,
                                    ProjectTranslation.PRIORITY,
                                    ProjectConsts.POSITIVE_NUMBER_MIN_VALUE.ToString()])
            .WithErrorCode(ProjectErrorCode.VALIDATION_ERROR_VALUE_GRATER_THAN);
        #endregion
    }
}
