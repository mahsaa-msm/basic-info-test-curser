namespace Master.Data.Core.Resources;

public sealed class ProjectTranslation
{
    #region COMMON

    public const string ID = nameof(ID);
    public const string CORE_ID = nameof(CORE_ID);
    public const string BUSINESS_ID = nameof(BUSINESS_ID);
    public const string SOFTWAREPART_ID = nameof(SOFTWAREPART_ID);
    public const string TENANT_ID = nameof(TENANT_ID);
    public const string INSURANCE_POLICY_NUMBER = nameof(INSURANCE_POLICY_NUMBER);
    public const string NAME = nameof(NAME);
    public const string CODE = nameof(CODE);
    public const string PRIORITY = nameof(PRIORITY);
    public const string TITLE = nameof(TITLE);
    public const string DISPLAY_TITLE = nameof(DISPLAY_TITLE);
    public const string UPDATE_DONE = nameof(UPDATE_DONE);
    public const string FILTER = nameof(FILTER);
    public const string SEARCH_INPUT = nameof(SEARCH_INPUT);


    public const string FIRST_NAME = nameof(FIRST_NAME);
    public const string LAST_NAME = nameof(LAST_NAME);
    public const string DESCRIPTION = nameof(DESCRIPTION);
    public const string IMAGE_URL = nameof(IMAGE_URL);

    public const string START_DATE = nameof(START_DATE);
    public const string END_DATE = nameof(END_DATE);

    public const string CAR_INSURANCE = nameof(CAR_INSURANCE);
    public const string ACCIDENT_INSURANCE = nameof(ACCIDENT_INSURANCE);
    public const string LIFE_INSURANCE = nameof(LIFE_INSURANCE);
    public const string FIRE_INSURANCE = nameof(FIRE_INSURANCE);
    public const string FACTOR_FIRE_INSURANCE = nameof(FACTOR_FIRE_INSURANCE);
    public const string HEALTH_INSURANCE = nameof(HEALTH_INSURANCE);
    public const string CARGO_INSURANCE = nameof(CARGO_INSURANCE);
    public const string ENGINEERING_INSURANCE = nameof(ENGINEERING_INSURANCE);
    public const string RESPONSIBILITY_INSURANCE = nameof(RESPONSIBILITY_INSURANCE);
    public const string EMPLOYER_RESPONSIBILITY_INSURANCE = nameof(EMPLOYER_RESPONSIBILITY_INSURANCE);
    public const string ACTIVE = nameof(ACTIVE);
    public const string SUSPEND = nameof(SUSPEND);
    public const string EXPIRE = nameof(EXPIRE);
    public const string PERCENTAGE = nameof(PERCENTAGE);
    public const string GENERAL = nameof(GENERAL);

    #region APPLICATION_ERROR
    /// <summary>
    /// خطای ناشناخته ای در سرویس {0} رخ داد. لطفا مجددا تلاش کنید یا با کارشناسان تماس بگیرید
    /// </summary>
    public const string APPLICATION_ERROR_UNACCEPTED_ERROR_OCCURRED_IN = nameof(APPLICATION_ERROR_UNACCEPTED_ERROR_OCCURRED_IN);

    #endregion

    #region HTTP_CLIENT
    /// <summary>
    /// تلاش {0} ام برای درخواست {1}
    /// </summary>
    public const string RETRY_REQUEST_POLLY = nameof(RETRY_REQUEST_POLLY);
    /// <summary>
    ///  تلاش {0} ام پس از {1}
    /// </summary>
    public const string RETRY_REQUEST_WITH_DELAY_POLLY = nameof(RETRY_REQUEST_WITH_DELAY_POLLY);
    /// <summary>
    /// درخواست {0} بعد از {1} ثانیه تایم اوت شد
    /// </summary>
    public const string TIMEOUT_REQUEST_DURATION_POLLY = nameof(TIMEOUT_REQUEST_DURATION_POLLY);
    /// <summary>
    /// تلاش ها شکست خورد! بازه زمانی {0}
    /// </summary>
    public const string CERCUIT_BROKEN_POLLY = nameof(CERCUIT_BROKEN_POLLY);
    public const string CERCUIT_RESET_POLLY = nameof(CERCUIT_RESET_POLLY);
    public const string SUCCESS_RESPONSE = nameof(SUCCESS_RESPONSE);
    public const string FAIL_RESPONSE = nameof(FAIL_RESPONSE);
    #endregion

    #endregion

    #region CUSTOMER
    public const string CUSTOMER_TYPE_PERSON = nameof(CUSTOMER_TYPE_PERSON);
    public const string CUSTOMER_TYPE_COMPANY = nameof(CUSTOMER_TYPE_COMPANY);

    #endregion

    #region CORE_SSO
    public const string CORE_SSO_AUTHENTICATION_FAILED = nameof(CORE_SSO_AUTHENTICATION_FAILED);
    #endregion

    #region CORE
    public const string CORE_API_TOKEN = nameof(CORE_API_TOKEN);
    public const string API_SSO_CORE_TOKEN = nameof(API_SSO_CORE_TOKEN);

    /// <summary>
    /// واکشی اطلاعات {0} از کور بیمه ناموفق بود
    /// </summary>
    public const string FETCH_DATA_FROM_CORE_FAILED = nameof(FETCH_DATA_FROM_CORE_FAILED);

    #endregion

    #region TENANT
    public const string CONFIG_TYPE = nameof(CONFIG_TYPE);
    public const string PAYMENT_CONFIG = nameof(PAYMENT_CONFIG);
    public const string SSO_CONFIG = nameof(SSO_CONFIG);
    public const string UI_STYLE_CONFIG = nameof(UI_STYLE_CONFIG);
    public const string TENANT = nameof(TENANT);
    public const string TENANT_CONFIG = nameof(TENANT_CONFIG);
    public const string TENANT_CONFIG_SETTINGS = nameof(TENANT_CONFIG_SETTINGS);

    #endregion

    #region COUNTRY
    public const string COUNTRY = nameof(COUNTRY);
    public const string COUNTRY_ID = nameof(COUNTRY_ID);

    #endregion

    #region INSURANCE_TYPE
    public const string INSURANCE_TYPE = nameof(INSURANCE_TYPE);
    public const string INSURANCE_TYPE_ID = nameof(INSURANCE_TYPE_ID);

    #endregion

    #region TRANSLATION

    public const string PARROT_TRANSLATION = nameof(PARROT_TRANSLATION);

    public const string TRANSLATION_KEY = nameof(TRANSLATION_KEY);
    public const string TRANSLATION_VALUE = nameof(TRANSLATION_VALUE);
    public const string TRANSLATION_CULTURE = nameof(TRANSLATION_CULTURE);

    #endregion

    #region PROVINCE
    public const string PROVINCE = nameof(PROVINCE);
    public const string PROVINCE_ID = nameof(PROVINCE_ID);
    #endregion

    #region CITY
    public const string CITY = nameof(CITY);
    public const string CITY_ID = nameof(CITY_ID);
    #endregion

    #region INSURANCE_UNIT
    public const string INSURANCE_UNIT = nameof(INSURANCE_UNIT);
    public const string INSURANCE_UNIT_ID = nameof(INSURANCE_UNIT_ID);
    public const string LATITUDE = nameof(LATITUDE);
    public const string LONGITUDE = nameof(LONGITUDE);
    public const string BRANCH = nameof(BRANCH);
    public const string HEADQUARTERS = nameof(HEADQUARTERS);
    public const string BROKER = nameof(BROKER);
    public const string REPRENSENTATION = nameof(REPRENSENTATION);
    public const string MARKETER = nameof(MARKETER);
    public const string AUXILIARY_MARKETER = nameof(AUXILIARY_MARKETER);
    public const string MARKETING_OFFICE = nameof(MARKETING_OFFICE);
    public const string INSURANCE_UNIT_TYPE = nameof(INSURANCE_UNIT_TYPE);
    public const string INSURANCE_UNIT_STATE = nameof(INSURANCE_UNIT_STATE);
    #endregion

    #region PATTERN_CATALOG
    public const string PATTERN_CATALOG = nameof(PATTERN_CATALOG);
    public const string PATTERN_CATALOG_ID = nameof(PATTERN_CATALOG_ID);
    public const string PATTERN_KEY = nameof(PATTERN_KEY);
    public const string REGEX_EXPRESSION = nameof(REGEX_EXPRESSION);

    #endregion

    #region SERVICE_FEATURE
    public const string SERVICE_FEATURE = nameof(SERVICE_FEATURE);
    public const string SERVICE_FEATURE_ID = nameof(SERVICE_FEATURE_ID);
    public const string LIFE_INSURANCE_PREMIUM_INSTALLMENT = nameof(LIFE_INSURANCE_PREMIUM_INSTALLMENT);
    public const string LIFE_INSURANCE_PREMIUM_INSTALLMENT_AUTO = nameof(LIFE_INSURANCE_PREMIUM_INSTALLMENT_AUTO);
    public const string LIFE_INSURANCE_LOAN_INSTALLMENT = nameof(LIFE_INSURANCE_LOAN_INSTALLMENT);
    public const string LIFE_INSURANCE_LOAN_INSTALLMENT_AUTO = nameof(LIFE_INSURANCE_LOAN_INSTALLMENT_AUTO);
    public const string FIRE_INSURANCE_PREMIUM = nameof(FIRE_INSURANCE_PREMIUM);
    public const string SERVICE_FEATURE_KEY = nameof(SERVICE_FEATURE_KEY);
    public const string SERVICE_NAME = nameof(SERVICE_NAME);
    public const string FEATURE_NAME = nameof(FEATURE_NAME);

    #endregion

    #region AGREEMENT_OBLIGATION
    public const string AGREEMENT_OBLIGATION = nameof(AGREEMENT_OBLIGATION);
    public const string AGREEMENT_OBLIGATION_ID = nameof(AGREEMENT_OBLIGATION_ID);
    public const string AGREEMENT_ID = nameof(AGREEMENT_ID);
    public const string ISSUANCE_SCHEME = nameof(ISSUANCE_SCHEME);
    public const string PREPAYMENT_PERCENTAGE = nameof(PREPAYMENT_PERCENTAGE);
    public const string FIRST_INSTALLMENT_DEADLINE = nameof(FIRST_INSTALLMENT_DEADLINE);
    public const string INSTALLMENTS_COUNT = nameof(INSTALLMENTS_COUNT);
    public const string INSTALLMENT_INTERVAL = nameof(INSTALLMENT_INTERVAL);
    public const string AGREEMENT_NUMBER = nameof(AGREEMENT_NUMBER);
    public const string AGREEMENT_OBLIGATION_NUMBER = nameof(AGREEMENT_OBLIGATION_NUMBER);
    public const string SALES_TYPE = nameof(SALES_TYPE);

    #endregion
}