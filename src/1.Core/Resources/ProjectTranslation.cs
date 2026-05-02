namespace Vehicle.Insurance.Core.Resources;

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
    public const string COLOR_HASH = nameof(COLOR_HASH);
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
    public const string TENANT_SLUG = nameof(TENANT_SLUG);

    #endregion

    #region VEHICLE_COLOR
    public const string VEHICLE_COLOR = nameof(VEHICLE_COLOR);
    public const string VEHICLE_COLOR_ID = nameof(VEHICLE_COLOR_ID);
    #endregion

    #region LICENSE_PLATE_TYPE
    public const string LICENSE_PLATE_TYPE = nameof(LICENSE_PLATE_TYPE);
    public const string LICENSE_PLATE_TYPE_ID = nameof(LICENSE_PLATE_TYPE_ID);
    #endregion

    #region VEHICLE_BRAND
    public const string VEHICLE_BRAND = nameof(VEHICLE_BRAND);
    public const string VEHICLE_BRAND_ID = nameof(VEHICLE_BRAND_ID);
    #endregion

    #region VEHICLE_TIP
    public const string VEHICLE_TIP = nameof(VEHICLE_TIP);
    public const string VEHICLE_TIP_ID = nameof(VEHICLE_TIP_ID);
    #endregion

    #region ServiceFeatureCategory
    // Property Insurance
    public const string PROPERTY_INSURANCE = nameof(PROPERTY_INSURANCE);
    public const string THIRD_PARTY_CAR_INSURANCE = nameof(THIRD_PARTY_CAR_INSURANCE);
    public const string COMPREHENSIVE_CAR_INSURANCE = nameof(COMPREHENSIVE_CAR_INSURANCE);
    public const string GENERAL_FIRE_INSURANCE = nameof(GENERAL_FIRE_INSURANCE);
    public const string RESIDENTIAL_HOME_FIRE_INSURANCE = nameof(RESIDENTIAL_HOME_FIRE_INSURANCE);
    public const string RESIDENTIAL_COMPLEX_FIRE_INSURANCE = nameof(RESIDENTIAL_COMPLEX_FIRE_INSURANCE);
    public const string COMPREHENSIVE_FIRE_PLAN = nameof(COMPREHENSIVE_FIRE_PLAN);
    public const string COMMERCIAL_FIRE_INSURANCE = nameof(COMMERCIAL_FIRE_INSURANCE);

    // Personal Insurance
    public const string PERSONAL_INSURANCE = nameof(PERSONAL_INSURANCE);
    public const string INDIVIDUAL_LIFE_INSURANCE = nameof(INDIVIDUAL_LIFE_INSURANCE);
    public const string LIFE_ENDOWMENT_INSURANCE = nameof(LIFE_ENDOWMENT_INSURANCE);
    public const string LIFE_ENDOWMENT_LOAN = nameof(LIFE_ENDOWMENT_LOAN);
    public const string LIFE_ENDOWMENT_PREMIUM = nameof(LIFE_ENDOWMENT_PREMIUM);
    public const string COMPREHENSIVE_LIFE_ACCIDENT_LIABILITY = nameof(COMPREHENSIVE_LIFE_ACCIDENT_LIABILITY);
    public const string COMPREHENSIVE_LIFE_ACCIDENT_LIABILITY_MINISTRY_ENERGY = nameof(COMPREHENSIVE_LIFE_ACCIDENT_LIABILITY_MINISTRY_ENERGY);
    public const string GROUP_LIFE_INSURANCE = nameof(GROUP_LIFE_INSURANCE);
    public const string INDIVIDUAL_MEDICAL_INSURANCE = nameof(INDIVIDUAL_MEDICAL_INSURANCE);
    public const string INDIVIDUAL_TRAVEL_MEDICAL_INSURANCE = nameof(INDIVIDUAL_TRAVEL_MEDICAL_INSURANCE);
    public const string GROUP_MEDICAL_INSURANCE = nameof(GROUP_MEDICAL_INSURANCE);
    public const string GROUP_COMPLEMENTARY_MEDICAL_INSURANCE = nameof(GROUP_COMPLEMENTARY_MEDICAL_INSURANCE);
    public const string INDIVIDUAL_ACCIDENT_INSURANCE = nameof(INDIVIDUAL_ACCIDENT_INSURANCE);
    public const string INDIVIDUAL_ACCIDENT_SUB_INSURANCE = nameof(INDIVIDUAL_ACCIDENT_SUB_INSURANCE);
    public const string GROUP_ACCIDENT_INSURANCE = nameof(GROUP_ACCIDENT_INSURANCE);

    // Liability Insurance
    public const string LIABILITY_INSURANCE = nameof(LIABILITY_INSURANCE);
    public const string GENERAL_LIABILITY_INSURANCE = nameof(GENERAL_LIABILITY_INSURANCE);
    public const string ELEVATOR_GENERAL_LIABILITY_INSURANCE = nameof(ELEVATOR_GENERAL_LIABILITY_INSURANCE);

    // Package Insurance
    public const string PACKAGE_INSURANCE = nameof(PACKAGE_INSURANCE);
    public const string COMPREHENSIVE_DOMESTIC_TRAVEL_PACKAGE = nameof(COMPREHENSIVE_DOMESTIC_TRAVEL_PACKAGE);
    #endregion
}
