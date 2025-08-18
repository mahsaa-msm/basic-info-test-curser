using System.ComponentModel;

namespace Master.Data.Core.Resources;

public sealed class ProjectConsts
{
    #region COMMON

    public const byte ID_MIN_VALUE = 1;
    public const byte ID_MIN_LENGTH = 2;
    public const byte ID_MAX_LENGTH = 200;
    public const byte PRICE_MIN_VALUE = 0;

    public const byte NAME_MIN_LENGTH = 2;
    public const byte NAME_MAX_LENGTH = 250;


    public const byte DESCRIPTION_MIN_LENGTH = 50;
    public const short DESCRIPTION_MAX_LENGTH = 500;


    public const byte URI_MIN_LENGTH = 10;
    public const short URI_MAX_LENGTH = 1000;

    public enum InsuranceType : byte
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

    #endregion

    #region GRPC_CLIENT
    public const string FACTORS_GRPC_CLIENT_NAME = "Factors";

    #endregion

    #region IDENTITY
    public const string FAKE_AUTHENTICATION_ITEM_NAME = "FakeAuthenticated";

    #endregion
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
    public enum CustomerType : byte
    {
        [Description(ProjectTranslation.CUSTOMER_TYPE_PERSON)] PERSON = 1, //حقیقی
        [Description(ProjectTranslation.CUSTOMER_TYPE_COMPANY)] COMPANY = 2,//حقوقی
    }
    #endregion

    #region CacheKeys
    public const string CORE_SSO_TOKEN_CACHE_KEY = "_CORE_SSO_TOKEN_";
    #endregion

}
