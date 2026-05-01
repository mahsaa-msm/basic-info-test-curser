using Vehicle.Insurance.Core.Domain.Common.Entities;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.VehicleColors.Parameters;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.VehicleColors.Entities;

public sealed class VehicleColor : BaseTenantEntity
{
    #region Properties
    public DIPTitle Title { get; private set; }
    public DIPTitle DisplayTitle { get; private set; }
    public CoreId CoreId { get; private set; }
    public ColorHash ColorHash { get; private set; }
    public Common.ValueObjects.Priority Priority { get; private set; }
    public IsActive IsActive { get; private set; }
    #endregion

    #region Constructors
    private VehicleColor() { }

    private VehicleColor(CreateVehicleColorParameter parameter)
    {
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull ?
            parameter.Title.Value :
            parameter.DisplayTitle.Value;
        CoreId = parameter.CoreId;
        ColorHash = parameter.ColorHash;
        Priority = parameter.Priority;
        IsActive = IsActive.True();
    }

    private VehicleColor(CreateVehicleColorWithTenantIdParameter parameter)
    {
        TenantId = parameter.TenantId;
        TenantBusinessId = parameter.TenantKey;
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull ?
            parameter.Title.Value :
            parameter.DisplayTitle.Value;
        CoreId = parameter.CoreId;
        ColorHash = parameter.ColorHash;
        Priority = parameter.Priority;
        IsActive = IsActive.True();
    }
    #endregion

    #region Commands
    public static VehicleColor Create(CreateVehicleColorParameter parameter)
        => new(parameter);

    public static VehicleColor CreateWithTenantId(CreateVehicleColorWithTenantIdParameter parameter)
        => new(parameter);

    public void Update(UpdateVehicleColorParameter parameter)
    {
        ColorHash = parameter.ColorHash;
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle;
        Priority = parameter.Priority;
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
