using Vehicle.Insurance.Core.Domain.Common.Entities;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.ServiceFeatures.Parameters;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.ServiceFeatures.Entities;

public sealed class ServiceFeature : BaseTenantEntity
{
    #region Properties
    public Name ServiceName { get; private set; }
    public Name FeatureName { get; private set; }
    public ServiceFeatureCategory Key { get; private set; }
    public Description? Description { get; private set; }
    public IsActive IsActive { get; private set; }
    public bool IsIssuable { get; private set; }
    public bool CanViewHistory { get; private set; }
    public NullableCoreId InsuranceTypeCoreId { get; private set; }


    #endregion

    #region Constructors
    private ServiceFeature()
    {

    }

    private ServiceFeature(CreateServiceFeatureParameter createServiceFeatureParameter)
    {
        ValueObjectGuard.ThrowIfNotValid(ServiceFeatureCategoryHelper.GetLevel((long)createServiceFeatureParameter.Key) >= 2,
                                         ProjectTranslation.SERVICE_FEATURE_KEY);
        var parent = createServiceFeatureParameter.Key.GetParent();
        Key = createServiceFeatureParameter.Key;
        ServiceName = parent.HasValue
            ? Name.FromString(parent.Value.ToString())
            : Name.FromString(Key.ToString()); // لول 1 خودش ServiceName هست

        FeatureName = Name.FromString(Key.ToString());
        IsIssuable = createServiceFeatureParameter.IsIssuable;
        CanViewHistory = createServiceFeatureParameter.CanViewHistory;
        InsuranceTypeCoreId = createServiceFeatureParameter.InsuranceTypeCoreId;
        Description = createServiceFeatureParameter.Description;
        IsActive = IsActive.True();
    }

    private ServiceFeature(CreateServiceFeatureWithTenantIdParameter createServiceFeatureWithTenantIdParameter)
    {
        ValueObjectGuard.ThrowIfNotValid(ServiceFeatureCategoryHelper.GetLevel((long)createServiceFeatureWithTenantIdParameter.Key) >= 2,
                                         ProjectTranslation.SERVICE_FEATURE_KEY);
        TenantId = createServiceFeatureWithTenantIdParameter.TenantId;
        var parent = createServiceFeatureWithTenantIdParameter.Key.GetParent();
        Key = createServiceFeatureWithTenantIdParameter.Key;
        ServiceName = parent.HasValue
            ? Name.FromString(parent.Value.ToString())
            : Name.FromString(Key.ToString()); // لول 1 خودش ServiceName هست
        FeatureName = Name.FromString(Key.ToString());
        IsIssuable = createServiceFeatureWithTenantIdParameter.IsIssuable;
        CanViewHistory = createServiceFeatureWithTenantIdParameter.CanViewHistory;
        InsuranceTypeCoreId = createServiceFeatureWithTenantIdParameter.InsuranceTypeCoreId;
        IsActive = IsActive.True();
    }
    #endregion

    #region Commands
    public static ServiceFeature Create(CreateServiceFeatureParameter createServiceFeatureParameter)
        => new(createServiceFeatureParameter);

    public static ServiceFeature Create(CreateServiceFeatureWithTenantIdParameter createServiceFeatureWithTenantIdParameter)
        => new(createServiceFeatureWithTenantIdParameter);

    public void Update(UpdateServiceFeatureParameter updateServiceFeatureParameter)
    {
        Description = updateServiceFeatureParameter.Description;
        IsIssuable = updateServiceFeatureParameter.IsIssuable;
        CanViewHistory = updateServiceFeatureParameter.CanViewHistory;
        InsuranceTypeCoreId = updateServiceFeatureParameter.InsuranceTypeCoreId;
    }

    public void Active()
    {
        if (!IsActive.Value)
            IsActive = IsActive.True();
    }


    public void Deactive()
    {
        if (IsActive.Value)
            IsActive = IsActive.False();
    }
    #endregion
}

