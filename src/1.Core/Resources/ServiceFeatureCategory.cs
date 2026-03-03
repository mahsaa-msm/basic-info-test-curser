using System.ComponentModel;

namespace Master.Data.Core.Resources;

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
    propertyInsurance_fire = 102, // بیمه آتش سوزی

    // 1_02_01
    [Description(ProjectTranslation.GENERAL_FIRE_INSURANCE)]
    propertyInsurance_fire_general = 10201, // بیمه آتش سوزی عمومی

    [Description(ProjectTranslation.RESIDENTIAL_HOME_FIRE_INSURANCE)]
    propertyInsurance_fire_general_home = 1020101, // بیمه آتش سوزی عمومی منازل مسکونی

    [Description(ProjectTranslation.RESIDENTIAL_COMPLEX_FIRE_INSURANCE)]
    propertyInsurance_fire_comprehensivePlan_residential = 1020201, // بیمه آتش سوزی مجتمع منازل مسكوني

    // 1_02_02
    [Description(ProjectTranslation.COMPREHENSIVE_FIRE_PLAN)]
    propertyInsurance_fire_comprehensivePlan = 10202, // بیمه آتش سوزی طرح جامع

    [Description(ProjectTranslation.COMMERCIAL_FIRE_INSURANCE)]
    propertyInsurance_fire_comprehensivePlan_commercial = 1020202, // بیمه آتش سوزی اصناف


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
    public static int GetLevel(long categoryCode)
    {
        if (categoryCode < 100) return 1;                     // 1-2 رقم (سطح 1)
        if (categoryCode < 10000) return 2;                  // 3-4 رقم (سطح 2)
        if (categoryCode < 1000000) return 3;                // 5-6 رقم (سطح 3)
        if (categoryCode < 100000000) return 4;              // 7-8 رقم (سطح 4)
        return 5;                                            // 9-10 رقم (سطح 5)
    }

    public static long? GetParent(long categoryCode)
    {
        var level = GetLevel(categoryCode);

        if (level <= 1) return null;

        // برای هر سطح، دو رقم آخر را حذف می‌کنیم
        long parentCode = categoryCode / 100;

        return parentCode > 0 ? parentCode : null;
    }

    public static bool IsChildOf(long childCode, long parentCode)
    {
        while (childCode > parentCode)
        {
            var parent = GetParent(childCode);
            if (!parent.HasValue) return false;

            if (parent.Value == parentCode) return true;

            childCode = parent.Value;
        }
        return false;
    }

    public static List<long> GetHierarchy(long categoryCode)
    {
        var hierarchy = new List<long>();
        var current = categoryCode;

        while (current > 0)
        {
            hierarchy.Insert(0, current);
            var parent = GetParent(current);
            if (!parent.HasValue) break;
            current = parent.Value;
        }

        return hierarchy;
    }

    public static string GetPath(ServiceFeatureCategory category)
    {
        var hierarchy = GetHierarchy((long)category);
        return string.Join(" → ", hierarchy);
    }

    // پیدا کردن تمام فرزندان مستقیم یک دسته
    public static List<ServiceFeatureCategory> GetDirectChildren(ServiceFeatureCategory parentCategory)
    {
        var parentCode = (long)parentCategory;
        var allValues = Enum.GetValues(typeof(ServiceFeatureCategory)).Cast<ServiceFeatureCategory>();

        return allValues.Where(category =>
        {
            var code = (long)category;
            var parent = GetParent(code);
            return parent.HasValue && parent.Value == parentCode;
        }).ToList();
    }

    // پیدا کردن تمام فرزندان (مستقیم و غیرمستقیم)
    public static List<ServiceFeatureCategory> GetAllChildren(ServiceFeatureCategory parentCategory)
    {
        var parentCode = (long)parentCategory;
        var allValues = Enum.GetValues(typeof(ServiceFeatureCategory)).Cast<ServiceFeatureCategory>();

        return allValues.Where(category =>
        {
            var code = (long)category;
            return IsChildOf(code, parentCode);
        }).ToList();
    }
}

