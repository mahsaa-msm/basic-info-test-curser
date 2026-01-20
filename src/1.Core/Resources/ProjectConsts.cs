using System.ComponentModel;

namespace Master.Data.Core.Resources;

public sealed class ProjectConsts
{
    #region COMMON

    public const byte ID_MIN_VALUE = 1;
    public const byte ID_MIN_LENGTH = 2;
    public const byte ID_MAX_LENGTH = 200;
    public const byte PRICE_MIN_VALUE = 0;
    public const byte POSITIVE_NUMBER_MIN_VALUE = 0;

    public const byte CORE_ID_MIN_LENGTH = 1;
    public const byte CORE_ID_MAX_LENGTH = 50;

    public const byte CODE_MIN_LENGTH = 1;
    public const byte CODE_MAX_LENGTH = 50;

    public const byte NAME_MIN_LENGTH = 1;
    public const byte NAME_MAX_LENGTH = 250;

    public const byte TITLE_MIN_LENGTH = 1;
    public const byte TITLE_MAX_LENGTH = 250;

    public const byte PERCENTAGE_MIN_VALUE = 0;
    public const byte PERCENTAGE_MAX_VALUE = 100;

    public const byte DESCRIPTION_MIN_LENGTH = 50;
    public const short DESCRIPTION_MAX_LENGTH = 500;


    public const byte URI_MIN_LENGTH = 10;
    public const short URI_MAX_LENGTH = 1000;

    public const short NATURAL_NUMBER_MIN_VALUE = 1;

    public const string GRPC_CORS_NAME = "gRPC_AllowAll";

    public enum MoveDirection
    {
        Up,
        Down,
        NoChange
    }

    public enum InsuranceTypeEnum : byte
    {
        [Description(ProjectTranslation.CAR_INSURANCE)] CarInsurance = 1, //بیمه خودرو
        [Description(ProjectTranslation.ACCIDENT_INSURANCE)] AccidentInsurance = 2, //بیمه  حوادث
        [Description(ProjectTranslation.LIFE_INSURANCE)] LifeInsurance = 3,//بیمه عمر
        [Description(ProjectTranslation.FIRE_INSURANCE)] FireInsurance = 4, //بیمه  آتش سوزی
        [Description(ProjectTranslation.HEALTH_INSURANCE)] HealthInsurance = 5, //بیمه درمانی
        [Description(ProjectTranslation.CARGO_INSURANCE)] CargoInsurance = 6, //بیمه باربری
        [Description(ProjectTranslation.ENGINEERING_INSURANCE)] EngineeringInsurance = 7,  //بیمه مهندسی انرژی و سایر بیمه ها
        [Description(ProjectTranslation.RESPONSIBILITY_INSURANCE)] ResponsibilityInsurance = 8,  //بیمه  مسئولیت بیمه مدنی
        [Description(ProjectTranslation.EMPLOYER_RESPONSIBILITY_INSURANCE)] EmployerResponsibilityInsurance = 9, //بیمه مسئولیت کارفرما در مقابل کارگران
    }

    #region HTTP_CLIENT
    public const string ACL_HTTP_CLIENT_NAME = "ACL";
    public const string CORE_SSO_HTTP_CLIENT_NAME = "CoreSsoApi";
    public const string AGENT_CLIENT_HTTP_CLIENT_NAME = "AgentClient";
    public const string CORE_INSURANCE_HTTP_CLIENT_NAME = "CoreInsuranceApi";

    #endregion

    #region GRPC_CLIENT
    public const string FACTORS_GRPC_CLIENT_NAME = "Factors";

    #endregion

    #region IDENTITY
    public const string FAKE_AUTHENTICATION_ITEM_NAME = "FakeAuthenticated";
    public const string TENANT_ID_X_HEADER_NAME = "X-Tenant-Id";
    public const string TENANT_ID_HEADER_NAME = "tenantId";
    public const string TENANT_KEY_X_HEADER_NAME = "X-Tenant-Key";
    public const string TENANT_KEY_HEADER_NAME = "tenantKey";

    #endregion

    #region CUSTOMER
    public const byte NATIONAL_CODE_LENGTH = 10;
    public const byte ECONOMIC_CODE_LENGTH = 12;
    public const string NATIONAL_CODE_CLAIM_NAME = "national-code";
    public const string ZAMIN_NATIONAL_CODE_CLAIM_NAME = "national_code";
    public const string CUSTOMER_TYPE_CLAIM_NAME = "dip-customer-type";
    public const string MINISTRY_OF_ENERGY_CUSTOMER_CLAIM_NAME = "dip-customer-ministryofenergy";
    public const string CUSTOMER_FIRST_NAME_CLAIM_NAME = "dip-customer-first-name";
    public const string CUSTOMER_LAST_NAME_CLAIM_NAME = "dip-customer-last-name";
    /// <summary>
    /// این مقدار همیشه صحیح نیست. مقدار صحیح در appSettings قرار دارد.
    /// </summary>
    public const string USER_CUSTOMER_ID_CLAIM_NAME = "dip-customer-id";
    /// <summary>
    /// این مقدار همیشه صحیح نیست. مقدار صحیح در appSettings قرار دارد.
    /// </summary>
    public const string BACKOFFICE_SUPER_ADMIN_CLAIM_NAME = "dip-backoffice-super-admin";
    /// <summary>
    /// این مقدار همیشه صحیح نیست. مقدار صحیح در appSettings قرار دارد.
    /// </summary>
    public const string BACKOFFICE_SUPER_ADMIN_CLAIM_VALUE = "dip-super-admin";
    public enum CustomerType : byte
    {
        [Description(ProjectTranslation.CUSTOMER_TYPE_PERSON)] PERSON = 1, //حقیقی
        [Description(ProjectTranslation.CUSTOMER_TYPE_COMPANY)] COMPANY = 2,//حقوقی
    }
    #endregion

    #region CacheKeys
    public const string CORE_SSO_TOKEN_CACHE_KEY = "_CORE_SSO_TOKEN_";
    #endregion


    #region TENANT
    public enum ConfigType
    {
        [Description(ProjectTranslation.PAYMENT_CONFIG)] PAYMENT_CONFIG = 1, //تنظیمات پرداخت 
        [Description(ProjectTranslation.SSO_CONFIG)] SSO_CONFIG = 2, //تنظیمات sso
        [Description(ProjectTranslation.UI_STYLE_CONFIG)] UI_STYLE_CONFIG = 3, //تنظیمات ظاهر برنامه
    }

    #endregion

    #endregion

    #region TRANSLATION

    public const byte TRANSLATION_KEY_MIN_LENGTH = 2;
    public const byte TRANSLATION_KEY_MAX_LENGTH = 200;

    public const byte TRANSLATION_VALUE_MIN_LENGTH = 2;
    public const byte TRANSLATION_VALUE_MAX_LENGTH = 200;

    public const byte TRANSLATION_CULTURE_LENGTH = 5;

    #endregion

    #region INSURANCE_UNIT
    public const Int16 LATITUDE_MIN_VALUE = -90;
    public const Int16 LATITUDE_MAX_VALUE = 90;
    public const Int16 LONGITUDE_MIN_VALUE = -180;
    public const Int16 LONGITUDE_MAX_VALUE = 180;
    public const double MAX_LATITUDE_DIFFERENCE = 10.0;
    public const double MAX_LONGITUDE_DIFFERENCE = 10.0;
    public const int SEARCH_INPUT_MAX_LENGTH = 100;
    public const string SEARCH_INPUT_PATTERN = @"^[\p{L}\p{N}\s\-_.]+$";

    public enum DistanceUnit
    {
        Kilometers,
        Meters,
        Miles
    }

    public enum InsuranceUnitType
    {
        [Description(ProjectTranslation.BRANCH)] Branch = 0, // شعبه
        [Description(ProjectTranslation.HEADQUARTERS)] Headquarters = 1, // ستاد
        [Description(ProjectTranslation.BROKER)] Broker = 2, // کارگزار
        [Description(ProjectTranslation.REPRENSENTATION)] Representation = 3, // نمایندگی
        [Description(ProjectTranslation.MARKETER)] Marketer = 4, // بازاریاب
        [Description(ProjectTranslation.AUXILIARY_MARKETER)] AuxiliaryMarketer = 5, // بازاریاب کمکی
        [Description(ProjectTranslation.MARKETING_OFFICE)] MarketingOffice = 7, // دفتر بازاریابی
    }

    public enum InsuranceUnitState
    {
        [Description(ProjectTranslation.ACTIVE)] Active = 0, // فعال
        [Description(ProjectTranslation.SUSPEND)] Suspend = 1, // تعلیق
        [Description(ProjectTranslation.EXPIRE)] Expire = 2, // اتمام
    }
    #endregion

    #region PATTERN_CATALOG
    public const string PATTERN_KEY_PATTERN = @"^[a-z0-9\-_]{{{0},{1}}}$";
    public const int PATTERN_KEY_MIN_LENGTH = 3;
    public const int PATTERN_KEY_MAX_LENGTH = 50;
    public const int PATTERN_MAX_LENGTH = 500;

    #endregion

    #region SERVICE_FEATURE
    public enum ServiceFeatureKey
    {
        [Description(ProjectTranslation.LIFE_INSURANCE_PREMIUM_INSTALLMENT)] LIFE_INSURANCE_PREMIUM_INSTALLMENT = 1, // قسط حق بیمه عمر
        [Description(ProjectTranslation.LIFE_INSURANCE_PREMIUM_INSTALLMENT_AUTO)] LIFE_INSURANCE_PREMIUM_INSTALLMENT_AUTO = 2, // قسط حق بیمه عمر - خودکار
        [Description(ProjectTranslation.LIFE_INSURANCE_LOAN_INSTALLMENT)] LIFE_INSURANCE_LOAN_INSTALLMENT = 3, // قسط وام بیمه نامه عمر
        [Description(ProjectTranslation.LIFE_INSURANCE_LOAN_INSTALLMENT_AUTO)] LIFE_INSURANCE_LOAN_INSTALLMENT_AUTO = 4, // قسط وام بیمه نامه عمر - خودکار
        [Description(ProjectTranslation.FIRE_INSURANCE_PREMIUM)] FIRE_INSURANCE_PREMIUM = 5, // صدور بیمه نامه آتش سوزی
    }

    #endregion

    #region AgreementObligation
    public enum SalesType
    {

    }

    public const int AGREEMENT_NUMBER_MIN_LENGTH = 2;
    public const int AGREEMENT_NUMBER_MAX_LENGTH = 100;

    #endregion
}
