using Master.Data.Core.Domain.Common.Entities;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.PatternCatalogs.Parameters;
using Master.Data.Core.Domain.PatternCatalogs.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.PatternCatalogs.Entities;

public sealed class PatternCatalog : BaseTenantEntity
{
    #region Properties
    public PatternKey Key { get; private set; }
    public RegexExpression Pattern { get; private set; }
    public Description? Description { get; private set; }
    public DateTime CreatedDateUtc { get; private set; }
    public DateTime? LastModifiedDateUtc { get; private set; }
    public Common.ValueObjects.Priority Priority { get; private set; }
    public IsActive IsActive { get; private set; }
    #endregion

    #region Constructors
    private PatternCatalog()
    {
    }

    private PatternCatalog(CreatePatternCatalogParameter parameter)
    {
        Key = parameter.Key;
        Pattern = parameter.Pattern;
        Description = parameter.Description;
        CreatedDateUtc = DateTime.UtcNow;
        Priority = parameter.Priority;
        IsActive = IsActive.True();
    }

    private PatternCatalog(CreatePatternCatalogWithTenantIdParameter parameter)
    {
        TenantId = parameter.TenantId;
        TenantBusinessId = parameter.TenantKey;
        Key = parameter.Key;
        Pattern = parameter.Pattern;
        Description = parameter.Description;
        CreatedDateUtc = DateTime.UtcNow;
        Priority = parameter.Priority;
        IsActive = IsActive.True();
    }
    #endregion

    #region Commands
    public static PatternCatalog Create(CreatePatternCatalogParameter parameter)
        => new(parameter);

    public static PatternCatalog CreateWithTenantId(CreatePatternCatalogWithTenantIdParameter parameter)
        => new(parameter);

    public void Update(UpdatePatternCatalogParameter parameter)
    {
        Pattern = parameter.Pattern;
        Description = parameter.Description;
        Priority = parameter.Priority;
        LastModifiedDateUtc = DateTime.UtcNow;
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

    public void PushDown() => Priority = Priority.Increase();

    public void PullUp() => Priority = Priority.Decrease();
    #endregion

    #region Queries
    public MoveDirection GetMoveDirection(Common.ValueObjects.Priority newPrioriy)
    {
        if (newPrioriy > Priority)
            return MoveDirection.Down;
        else if (newPrioriy < Priority)
            return MoveDirection.Up;
        return MoveDirection.NoChange;
    }
    #endregion
}
