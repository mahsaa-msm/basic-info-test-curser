using Master.Data.Core.Domain.Common.Entities;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.ServiceFeatures.Parameters;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.ServiceFeatures.Entities;

public sealed class ServiceFeature : BaseTenantEntity
{
    #region Properties
    public Name ServiceName { get; private set; }
    public Name FeatureName { get; private set; }
    public ServiceFeatureKey Key { get; private set; }
    public Description? Description { get; private set; }
    public IsActive IsActive { get; private set; }
    #endregion

    #region Constructors
    private ServiceFeature()
    {

    }

    private ServiceFeature(CreateServiceFeatureParameter createServiceFeatureParameter)
    {
        Key = createServiceFeatureParameter.Key;
        ServiceName = createServiceFeatureParameter.ServiceName;
        FeatureName = createServiceFeatureParameter.FeatureName;
        Description = createServiceFeatureParameter.Description;
        IsActive = IsActive.True();
    }
    #endregion

    #region Commands
    public static ServiceFeature Create(CreateServiceFeatureParameter createServiceFeatureParameter)
        => new(createServiceFeatureParameter);

    public void Update(UpdateServiceFeatureParameter updateServiceFeatureParameter)
    {
        ServiceName = updateServiceFeatureParameter.ServiceName;
        FeatureName = updateServiceFeatureParameter.FeatureName;
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
