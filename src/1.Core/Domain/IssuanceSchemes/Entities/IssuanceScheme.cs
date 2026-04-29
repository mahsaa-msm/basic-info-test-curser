using Vehicle.Insurance.Core.Domain.Common.Entities;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.IssuanceSchemes.Parameters;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.Domain.IssuanceSchemes.Entities;
public sealed class IssuanceScheme : BaseTenantEntity
{
    #region Properties
    public DIPTitle Title { get; private set; }
    public DIPTitle DisplayTitle { get; private set; }
    public CoreId CoreId { get; private set; }
    public Code Code { get; private set; }
    public Common.ValueObjects.Priority Priority { get; private set; }
    public IsActive IsActive { get; private set; }
    public DateTime? FromStartDateUtc { get; private set; }
    public DateTime? ToStartDateUtc { get; private set; }
    public DateTime? FromIssueDateUtc { get; private set; }
    public DateTime? ToIssueDateUtc { get; private set; }
    public CoreId InsuranceTypeCoreId { get; private set; }
    public AdjustmentType? AdjustmentType { get; private set; }
    public NullablePercentage? AdjustmentPercent { get; private set; }
    #endregion

    #region Constructors
    private IssuanceScheme() { }

    private IssuanceScheme(CreateIssuanceSchemeParameter parameter)
    {
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull ?
            parameter.Title.Value :
            parameter.DisplayTitle.Value;
        CoreId = parameter.CoreId;
        Code = parameter.Code;
        Priority = parameter.Priority;
        FromStartDateUtc = parameter.FromStartDateUtc;
        ToStartDateUtc = parameter.ToStartDateUtc;
        FromIssueDateUtc = parameter.FromIssueDateUtc;
        ToIssueDateUtc = parameter.ToIssueDateUtc;
        InsuranceTypeCoreId = parameter.InsuranceTypeCoreId;
        AdjustmentType = parameter.AdjustmentType;
        AdjustmentPercent = parameter.AdjustmentPercent;
        IsActive = IsActive.True();
    }

    private IssuanceScheme(CreateIssuanceSchemeWithTenantIdParameter parameter)
    {
        TenantId = parameter.TenantId;
        TenantBusinessId = parameter.TenantKey;
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull ?
            parameter.Title.Value :
            parameter.DisplayTitle.Value;
        CoreId = parameter.CoreId;
        Code = parameter.Code;
        FromStartDateUtc = parameter.FromStartDateUtc;
        ToStartDateUtc = parameter.ToStartDateUtc;
        FromIssueDateUtc = parameter.FromIssueDateUtc;
        ToIssueDateUtc = parameter.ToIssueDateUtc;
        InsuranceTypeCoreId = parameter.InsuranceTypeCoreId;
        AdjustmentType = parameter.AdjustmentType;
        AdjustmentPercent = parameter.AdjustmentPercent;
        Priority = parameter.Priority;
        IsActive = IsActive.True();
    }
    #endregion

    #region Commands
    public static IssuanceScheme Create(CreateIssuanceSchemeParameter parameter)
        => new(parameter);

    public static IssuanceScheme CreateWithTenantId(CreateIssuanceSchemeWithTenantIdParameter parameter)
    => new(parameter);

    public void Update(UpdateIssuanceSchemeParameter parameter)
    {
        Code = parameter.Code;
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle;
        Priority = parameter.Priority;
        FromStartDateUtc = parameter.FromStartDateUtc;
        ToStartDateUtc = parameter.ToStartDateUtc;
        FromIssueDateUtc = parameter.FromIssueDateUtc;
        ToIssueDateUtc = parameter.ToIssueDateUtc;
        InsuranceTypeCoreId = parameter.InsuranceTypeCoreId;
        AdjustmentType = parameter.AdjustmentType;
        AdjustmentPercent = parameter.AdjustmentPercent;
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

    public void PushDown()
    {

        Priority = Priority.Increase();
    }

    public void PullUp()
    {

        Priority = Priority.Decrease();
    }
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

