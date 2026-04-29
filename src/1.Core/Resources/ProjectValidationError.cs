namespace Vehicle.Insurance.Core.Resources;

public sealed class ProjectValidationError
{
    #region Global
    //مقدار {0} اجباری می باشد
    public const string VALIDATION_ERROR_REQUIRED = nameof(VALIDATION_ERROR_REQUIRED);

    /// <summary>
    /// حداقل یکی از {0} الزامی است
    /// </summary>
    public const string VALIDATION_ERROR_AT_LEAST_ONE_OF_REQUIRED = nameof(VALIDATION_ERROR_AT_LEAST_ONE_OF_REQUIRED);

    public const string NO_NEW_INFORMATION_WAS_FOUND_TO_UPDATE = nameof(NO_NEW_INFORMATION_WAS_FOUND_TO_UPDATE);

    public const string VALIDATION_ERROR_NOT_EXIST = nameof(VALIDATION_ERROR_NOT_EXIST);

    public const string VALIDATION_ERROR_NOT_EXIST_ANY = nameof(VALIDATION_ERROR_NOT_EXIST_ANY);

    public const string VALIDATION_ERROR_DUPLICATE = nameof(VALIDATION_ERROR_DUPLICATE);

    /// <summary>
    /// {0} معتبر نمی‌باشد.
    /// </summary>
    public const string VALIDATION_ERROR_NOT_VALID = nameof(VALIDATION_ERROR_NOT_VALID);

    public const string VALIDATION_ERROR_NOT_EQUAL_TO = nameof(VALIDATION_ERROR_NOT_EQUAL_TO);

    public const string VALIDATION_ERROR_FORMAT = nameof(VALIDATION_ERROR_FORMAT);

    public const string VALIDATION_ERROR_VALUE_GRATER_THAN = nameof(VALIDATION_ERROR_VALUE_GRATER_THAN);
    //حذف {0} استفاده شده امکان پذیر نمی باشد
    public const string VALIDATION_ERROR_NOT_POSSIBLE_TO_DELETE_USED_ITEM = nameof(VALIDATION_ERROR_NOT_POSSIBLE_TO_DELETE_USED_ITEM);

    public const string VALIDATION_ERROR_CHANGE_STATUS = nameof(VALIDATION_ERROR_CHANGE_STATUS);

    /// <summary>
    /// مقدار {0} حذف نشده و نمیتوان آن را بازیابی کرد
    /// </summary>
    public const string VALIDATION_ERROR_CAN_NOT_RESTORE_NOT_DELETED = nameof(VALIDATION_ERROR_CAN_NOT_RESTORE_NOT_DELETED);
    #endregion

    #region Core

    public const string ERROR_IN_GET_TOKEN = nameof(ERROR_IN_GET_TOKEN);
    public const string ERROR_IN_GET_DATA_FROM_CORE_INSURANCE_API = nameof(ERROR_IN_GET_DATA_FROM_CORE_INSURANCE_API);
    #endregion

    #region Number
    public const string VALIDATION_ERROR_NUMBER_BETWEEN = nameof(VALIDATION_ERROR_NUMBER_BETWEEN);

    public const string VALIDATION_ERROR_NUMBER_LESS_THAN = nameof(VALIDATION_ERROR_NUMBER_LESS_THAN);
    public const string VALIDATION_ERROR_NUMBER_LESS_THAN_OR_EQUAL_THAN = nameof(VALIDATION_ERROR_NUMBER_LESS_THAN_OR_EQUAL_THAN);

    public const string VALIDATION_ERROR_NUMBER_GRATER_THAN = nameof(VALIDATION_ERROR_NUMBER_GRATER_THAN);
    public const string VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN = nameof(VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN);

    public const string VALIDATION_ERROR_MUST_BE_NUMERIC = nameof(VALIDATION_ERROR_MUST_BE_NUMERIC);
    #endregion

    #region String
    //طول مناسب برای {0} بزرگ تر برای {1} و کوچک تر برابر {2} می باشد
    public const string VALIDATION_ERROR_STRING_LENGTH_BETWEEN = nameof(VALIDATION_ERROR_STRING_LENGTH_BETWEEN);

    public const string VALIDATION_ERROR_STRING_MIN_LENGTH = nameof(VALIDATION_ERROR_STRING_MIN_LENGTH);

    public const string VALIDATION_ERROR_STRING_MAX_LENGTH = nameof(VALIDATION_ERROR_STRING_MAX_LENGTH);

    public const string VALIDATION_ERROR_STRING_LENGTH_MUST_EQUAL = nameof(VALIDATION_ERROR_STRING_LENGTH_MUST_EQUAL);

    public const string VALIDATION_ERROR_STRING_MUST_HAS_UPPER_CASE = nameof(VALIDATION_ERROR_STRING_MUST_HAS_UPPER_CASE);

    public const string VALIDATION_ERROR_STRING_MUST_HAS_LOWER_CASE = nameof(VALIDATION_ERROR_STRING_MUST_HAS_LOWER_CASE);

    public const string VALIDATION_ERROR_STRING_MUST_HAS_DIGIT = nameof(VALIDATION_ERROR_STRING_MUST_HAS_DIGIT);

    public const string VALIDATION_ERROR_STRING_MUST_HAS_NON_ALPHA_NUMERIC = nameof(VALIDATION_ERROR_STRING_MUST_HAS_NON_ALPHA_NUMERIC);

    public const string VALIDATION_ERROR_STRING_MUST_HAS_UNIQUE_CHAR = nameof(VALIDATION_ERROR_STRING_MUST_HAS_UNIQUE_CHAR);

    public const string VALIDATION_ERROR_INVLAID_IP_ADDRESS = nameof(VALIDATION_ERROR_INVLAID_IP_ADDRESS);
    #endregion

    #region Date
    public const string VALIDATION_ERROR_DATE_LESS_THAN = nameof(VALIDATION_ERROR_DATE_LESS_THAN);

    public const string VALIDATION_ERROR_DATE_LESS_THAN_OR_EQUAL = nameof(VALIDATION_ERROR_DATE_LESS_THAN_OR_EQUAL);

    public const string VALIDATION_ERROR_DATE_LESS_THAN_TO_TODAY = nameof(VALIDATION_ERROR_DATE_LESS_THAN_TO_TODAY);

    public const string VALIDATION_ERROR_DATE_LESS_THAN_OR_EQUAL_TO_TODAY = nameof(VALIDATION_ERROR_DATE_LESS_THAN_OR_EQUAL_TO_TODAY);

    public const string VALIDATION_ERROR_DATE_GREATER_THAN = nameof(VALIDATION_ERROR_DATE_GREATER_THAN);

    public const string VALIDATION_ERROR_DATE_GREATER_THAN_OR_EQUAL = nameof(VALIDATION_ERROR_DATE_GREATER_THAN_OR_EQUAL);

    public const string VALIDATION_ERROR_DATE_GREATER_THAN_TO_TODAY = nameof(VALIDATION_ERROR_DATE_GREATER_THAN_TO_TODAY);

    public const string VALIDATION_ERROR_DATE_GREATER_THAN_OR_EQUAL_TO_TODAY = nameof(VALIDATION_ERROR_DATE_GREATER_THAN_OR_EQUAL_TO_TODAY);

    /// <summary>
    /// تاریخ صحیح برای {0} باید بین {1} و {2} باشد
    /// </summary>
    public const string VALIDATION_ERROR_DATE_BETWEEN = nameof(VALIDATION_ERROR_DATE_BETWEEN);
    #endregion

    #region Password

    public const string NOT_VALID_TO_CHANGE_PASSWORD = nameof(NOT_VALID_TO_CHANGE_PASSWORD);

    public const string INVALID_DATA = nameof(INVALID_DATA);

    #endregion

    #region GeoLocation
    public const string VALIDATION_ERROR_BOTH_LATITUDE_LONGITUDE_MUST_EXIST = nameof(VALIDATION_ERROR_BOTH_LATITUDE_LONGITUDE_MUST_EXIST);
    public const string VALIDATION_ERROR_BOTH_OR_ANY_LATITUDE_LONGITUDE_MUST_EXIST = nameof(VALIDATION_ERROR_BOTH_OR_ANY_LATITUDE_LONGITUDE_MUST_EXIST);
    /// <summary>
    /// محدوده جغرافیایی ارائه شده معتبر نیست
    /// </summary>
    public const string VALIDATION_ERROR_INVALID_GEOGRAPHIC_AREA = nameof(VALIDATION_ERROR_INVALID_GEOGRAPHIC_AREA);

    /// <summary>
    /// حداقل عرض جغرافیایی باید از حداکثر کمتر باشد
    /// </summary>
    public const string VALIDATION_ERROR_MIN_LATITUDE_LESS_THAN_MAX = nameof(VALIDATION_ERROR_MIN_LATITUDE_LESS_THAN_MAX);

    /// <summary>
    /// حداقل طول جغرافیایی باید از حداکثر کمتر باشد
    /// </summary>
    public const string VALIDATION_ERROR_MIN_LONGITUDE_LESS_THAN_MAX = nameof(VALIDATION_ERROR_MIN_LONGITUDE_LESS_THAN_MAX);

    /// <summary>
    /// اختلاف عرض جغرافیایی نمی‌تواند بیشتر از {0} درجه باشد
    /// </summary>
    public const string VALIDATION_ERROR_MAX_LATITUDE_DIFFERENCE = nameof(VALIDATION_ERROR_MAX_LATITUDE_DIFFERENCE);

    /// <summary>
    /// اختلاف طول جغرافیایی نمی‌تواند بیشتر از {0} درجه باشد
    /// </summary>
    public const string VALIDATION_ERROR_MAX_LONGITUDE_DIFFERENCE = nameof(VALIDATION_ERROR_MAX_LONGITUDE_DIFFERENCE);

    /// <summary>
    /// فیلد {0} باید بین {1} و {2} باشد
    /// </summary>
    public const string VALIDATION_ERROR_RANGE = nameof(VALIDATION_ERROR_RANGE);

    /// <summary>
    /// متن جستجو شامل کاراکترهای غیرمجاز است
    /// </summary>
    public const string VALIDATION_ERROR_INVALID_SEARCH_INPUT_PATTERN = nameof(VALIDATION_ERROR_INVALID_SEARCH_INPUT_PATTERN);

    #endregion
}
