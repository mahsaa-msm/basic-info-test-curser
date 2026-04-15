using Master.Data.Core.Domain.Common.Entities;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.ServiceFeatures.Parameters;
using Master.Data.Core.Resources;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Master.Data.Core.Domain.ServiceFeatures.Entities;

public sealed class ServiceFeature : BaseTenantEntity
{
    #region Properties
    public Name ServiceName { get; private set; }
    public Name FeatureName { get; private set; }
    public ServiceFeatureCategory Key { get; private set; }
    public Description? Description { get; private set; }
    public IsActive IsActive { get; private set; }

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
