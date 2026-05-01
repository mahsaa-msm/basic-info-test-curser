using System.ComponentModel;
using System.Reflection;

namespace Vehicle.Insurance.Core.Resources;

public enum ServiceFeatureCategory : long
{
    // ============================================
    // 1: Property & Asset Insurance
    // ============================================

    [Description(ProjectTranslation.PROPERTY_INSURANCE)]
    propertyInsurance = 1, // بیمه اموال و دارایی

    // 1_01
    [Description(ProjectTranslation.CAR_INSURANCE)]
    propertyInsurance_car = 101, // بیمه خودرو

    [Description(ProjectTranslation.THIRD_PARTY_CAR_INSURANCE)]
    propertyInsurance_car_thirdParty = 10101, // بیمه ثالث خودرو

    [Description(ProjectTranslation.COMPREHENSIVE_CAR_INSURANCE)]
    propertyInsurance_car_comprehensive = 10102, // بیمه بدنه خودرو

    // 1_02
    [Description(ProjectTranslation.FIRE_INSURANCE)]
    propertyInsurance_vehicle = 102, // بیمه آتش سوزی

    // 1_02_01
    [Description(ProjectTranslation.RESIDENTIAL_HOME_FIRE_INSURANCE)]
    propertyInsurance_vehicle_general_home = 10201, // بیمه آتش سوزی عمومی منازل مسکونی

    // 1_02_02
    [Description(ProjectTranslation.RESIDENTIAL_COMPLEX_FIRE_INSURANCE)]
    propertyInsurance_vehicle_comprehensivePlan_residential = 10202, // بیمه آتش سوزی مجتمع منازل مسكوني

    // 1_02_03
    [Description(ProjectTranslation.COMMERCIAL_FIRE_INSURANCE)]
    propertyInsurance_vehicle_comprehensivePlan_commercial = 10203, // بیمه آتش سوزی اصناف


    // ============================================
    // 2: Personal Insurance
    // ============================================

    [Description(ProjectTranslation.PERSONAL_INSURANCE)]
    personalInsurance = 2, // بیمه های اشخاص

    // 2_01
    [Description(ProjectTranslation.INDIVIDUAL_LIFE_INSURANCE)]
    personalInsurance_lifeIndividual = 201, // بیمه عمر انفرادی

    [Description(ProjectTranslation.LIFE_ENDOWMENT_INSURANCE)]
    personalInsurance_lifeIndividual_endowment = 20101, // بیمه عمر و تأمین آتیه

    [Description(ProjectTranslation.LIFE_ENDOWMENT_LOAN)]
    personalInsurance_lifeIndividual_endowment_loan = 2010101, // وام بیمه عمر

    [Description(ProjectTranslation.LIFE_ENDOWMENT_PREMIUM)]
    personalInsurance_lifeIndividual_endowment_premium = 2010102, // حق بیمه عمر

    [Description(ProjectTranslation.COMPREHENSIVE_LIFE_ACCIDENT_LIABILITY)]
    personalInsurance_lifeIndividual_comprehensiveLifeAccidentLiability = 20102, // جامع عمر، حوادث و مسئولیت

    [Description(ProjectTranslation.COMPREHENSIVE_LIFE_ACCIDENT_LIABILITY_MINISTRY_ENERGY)]
    personalInsurance_lifeIndividual_comprehensiveLifeAccidentLiability_minstryEnergy = 2010201, // جامع عمر، حوادث و مسئولیت وزارت نیرو

    // 2_02
    [Description(ProjectTranslation.GROUP_LIFE_INSURANCE)]
    personalInsurance_lifeGroup = 202, // بیمه عمر گروهی

    // 2_03
    [Description(ProjectTranslation.INDIVIDUAL_MEDICAL_INSURANCE)]
    personalInsurance_medicalIndividual = 203, // بیمه درمان انفرادی

    [Description(ProjectTranslation.INDIVIDUAL_TRAVEL_MEDICAL_INSURANCE)]
    personalInsurance_medicalIndividual_travel = 20301, // بیمه درمان انفرادی مسافرتی

    // 2_04
    [Description(ProjectTranslation.GROUP_MEDICAL_INSURANCE)]
    personalInsurance_medicalGroup = 204, // بیمه درمان گروهی

    [Description(ProjectTranslation.GROUP_COMPLEMENTARY_MEDICAL_INSURANCE)]
    personalInsurance_medicalGroup_complementary = 20401, // بیمه گروهی درمان تکمیلی

    // 2_05
    [Description(ProjectTranslation.INDIVIDUAL_ACCIDENT_INSURANCE)]
    personalInsurance_accidentIndividual = 205, // بیمه حوادث انفرادی

    [Description(ProjectTranslation.INDIVIDUAL_ACCIDENT_SUB_INSURANCE)]
    personalInsurance_accidentIndividual_sub = 20501, // بیمه حادثه انفرادی

    // 2_06
    [Description(ProjectTranslation.GROUP_ACCIDENT_INSURANCE)]
    personalInsurance_accidentGroup = 206, // بیمه حوادث گروهی


    // ============================================
    // 3: Liability Insurance
    // ============================================

    [Description(ProjectTranslation.LIABILITY_INSURANCE)]
    liabilityInsurance = 3, // بیمه های مسئولیت

    [Description(ProjectTranslation.GENERAL_LIABILITY_INSURANCE)]
    liabilityInsurance_general = 301, // مسئولیت عمومی

    [Description(ProjectTranslation.ELEVATOR_GENERAL_LIABILITY_INSURANCE)]
    liabilityInsurance_general_elevator = 30101, // مسئولیت عمومی آسانسور


    // ============================================
    // 4: Package Insurance
    // ============================================

    [Description(ProjectTranslation.PACKAGE_INSURANCE)]
    packageInsurance = 4, // بیمه های ترکیبی (پکیج)

    [Description(ProjectTranslation.COMPREHENSIVE_DOMESTIC_TRAVEL_PACKAGE)]
    packageInsurance_comprehensiveTravelDomestic = 401 // جامع مسافرتی داخلی کشور
}

public static class ServiceFeatureCategoryHelper
{
    #region GetLevel
    /// <summary>
    /// سطح سلسله‌مراتبی کد را بر اساس تعداد ارقام برمی‌گرداند:
    ///   Level 1 → 1 رقم  (مثال: 1, 2)
    ///   Level 2 → 3 رقم  (مثال: 101, 102)
    ///   Level 3 → 5 رقم  (مثال: 10101, 20102)
    ///   Level 4 → 7 رقم  (مثال: 2010101)
    ///   Level 5 → 9 رقم
    /// </summary>
    public static int GetLevel(this long categoryCode)
    {
        int digits = categoryCode.ToString().Length;

        return digits switch
        {
            1 => 1,
            3 => 2,
            5 => 3,
            7 => 4,
            9 => 5,
            _ => throw new ArgumentException(
                     $"کد {categoryCode} با {digits} رقم ساختار معتبری ندارد.")
        };
    }

    /// <summary>
    /// سطح سلسله‌مراتبی دسته‌بندی را برمی‌گرداند
    /// </summary>
    public static int GetLevel(this ServiceFeatureCategory category)
        => ((long)category).GetLevel();
    #endregion

    #region GetParent
    /// <summary>
    /// کد والد مستقیم را برمی‌گرداند.
    /// با حذف دو رقم آخر (تقسیم بر 100) والد محاسبه می‌شود.
    /// برای Level 1 مقدار null برمی‌گردد.
    /// </summary>
    public static long? GetParentCode(this long categoryCode)
    {
        if (categoryCode.GetLevel() <= 1) return null;

        return categoryCode / 100;
    }

    /// <summary>
    /// والد مستقیم دسته‌بندی را برمی‌گرداند.
    /// در صورتی که والد در enum تعریف نشده باشد null برمی‌گرداند.
    /// </summary>
    public static ServiceFeatureCategory? GetParent(this ServiceFeatureCategory category)
    {
        var parentCode = ((long)category).GetParentCode();
        if (parentCode is null) return null;

        return Enum.IsDefined(typeof(ServiceFeatureCategory), parentCode.Value)
            ? (ServiceFeatureCategory)parentCode.Value
            : null;
    }
    #endregion

    #region GetByLevel
    /// <summary>
    /// لیست دسته‌بندی‌های یک سطح مشخص را برمی‌گرداند.
    /// اگر level نامعتبر باشد یا نتیجه‌ای نداشته باشد، سطح 1 برگردانده می‌شود.
    /// </summary>
    public static List<ServiceFeatureCategory> GetByLevel(int? level)
    {
        var allValues = Enum.GetValues<ServiceFeatureCategory>();

        if (level.HasValue)
        {
            var result = allValues
                .Where(c => ((long)c).GetLevel() == level.Value)
                .ToList();

            if (result.Count > 0) return result;
        }

        // fallback: سطح 1
        return allValues
            .Where(c => ((long)c).GetLevel() == 1)
            .ToList();
    }
    #endregion

    #region IschildOf
    /// <summary>
    /// بررسی می‌کند که آیا این دسته‌بندی فرزند (مستقیم یا غیرمستقیم) دسته‌بندی دیگری است
    /// </summary>
    public static bool IsChildOf(this ServiceFeatureCategory child, ServiceFeatureCategory parent)
        => ((long)child).IsChildOf((long)parent);

    /// <summary>
    /// بررسی می‌کند که آیا childCode فرزند parentCode است
    /// </summary>
    public static bool IsChildOf(this long childCode, long parentCode)
    {
        var current = childCode;
        while (current > parentCode)
        {
            var parent = current.GetParentCode();
            if (parent is null) return false;
            if (parent.Value == parentCode) return true;
            current = parent.Value;
        }
        return false;
    }
    #endregion

    #region GetAncestors
    /// <summary>
    /// لیست کدها را از ریشه تا کد جاری برمی‌گرداند
    /// </summary>
    public static List<long> GetAncestorsCodes(this long categoryCode)
    {
        var hierarchy = new List<long>();
        var current = categoryCode;

        while (true)
        {
            hierarchy.Insert(0, current);
            var parent = current.GetParentCode();
            if (parent is null) break;
            current = parent.Value;
        }

        return hierarchy;
    }

    /// <summary>
    /// لیست دسته‌بندی‌ها را از ریشه تا دسته‌بندی جاری برمی‌گرداند
    /// </summary>
    public static List<ServiceFeatureCategory> GetAncestors(this ServiceFeatureCategory category)
    {
        return ((long)category)
            .GetAncestorsCodes()
            .Where(c => Enum.IsDefined(typeof(ServiceFeatureCategory), c))
            .Select(c => (ServiceFeatureCategory)c)
            .ToList();
    }
    #endregion

    #region GetPath
    /// <summary>
    /// مسیر کامل دسته‌بندی را با نام enum برمی‌گرداند
    /// مثال: propertyInsurance → propertyInsurance_car → propertyInsurance_car_thirdParty
    /// </summary>
    public static string GetPath(this ServiceFeatureCategory category)
    {
        return string.Join(
            " → ",
            category.GetAncestors().Select(c => c.ToString())
        );
    }

    /// <summary>
    /// مسیر کامل دسته‌بندی را با توضیحات (Description) برمی‌گرداند
    /// </summary>
    public static string GetDescriptionPath(this ServiceFeatureCategory category)
    {
        return string.Join(
            " → ",
            category.GetAncestors().Select(c => c.GetDescription())
        );
    }
    #endregion

    #region GetDescription
    /// <summary>
    /// مقدار Description attribute دسته‌بندی را برمی‌گرداند.
    /// در صورت نبود، نام enum برگردانده می‌شود.
    /// </summary>
    public static string GetDescription(this ServiceFeatureCategory category)
    {
        return typeof(ServiceFeatureCategory)
            .GetField(category.ToString())
            ?.GetCustomAttribute<DescriptionAttribute>()
            ?.Description
            ?? category.ToString();
    }
    #endregion

    #region GetChildren
    /// <summary>
    /// لیست فرزندان مستقیم دسته‌بندی را برمی‌گرداند
    /// </summary>
    public static List<ServiceFeatureCategory> GetChildren(this ServiceFeatureCategory category)
    {
        var categoryCode = (long)category;

        return Enum.GetValues<ServiceFeatureCategory>()
            .Where(c => ((long)c).GetParentCode() == categoryCode)
            .ToList();
    }
    #endregion

    #region GetDescendants
    /// <summary>
    /// لیست تمام فرزندان (مستقیم و غیرمستقیم) دسته‌بندی را برمی‌گرداند
    /// </summary>
    public static List<ServiceFeatureCategory> GetAllChildren(this ServiceFeatureCategory category)
    {
        var categoryCode = (long)category;

        return Enum.GetValues<ServiceFeatureCategory>()
            .Where(c => ((long)c).IsChildOf(categoryCode))
            .ToList();
    }
    #endregion
}
