using Vehicle.Insurance.Core.Domain.Common.Entities;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.VehicleTips.Parameters;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.Domain.VehicleTips.Entities;

public sealed class VehicleTip : BaseTenantEntity
{
    public DIPTitle Title { get; private set; }
    public DIPTitle DisplayTitle { get; private set; }
    public CoreId CoreId { get; private set; }
    public CoreId BrandCoreId { get; private set; }
    public CoreId VehicleTypeCoreId { get; private set; }
    public CoreId VehicleSystemCoreId { get; private set; }
    public Common.ValueObjects.Priority Priority { get; private set; }
    public IsActive IsActive { get; private set; }

    private VehicleTip() { }

    private VehicleTip(CreateVehicleTipParameter parameter)
    {
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull
            ? parameter.Title.Value
            : parameter.DisplayTitle.Value;
        CoreId = parameter.CoreId;
        BrandCoreId = parameter.BrandCoreId;
        VehicleTypeCoreId = parameter.VehicleTypeCoreId;
        VehicleSystemCoreId = parameter.VehicleSystemCoreId;
        Priority = parameter.Priority;
        IsActive = IsActive.True();
    }

    public static VehicleTip Create(CreateVehicleTipParameter parameter)
        => new(parameter);

    public void Update(UpdateVehicleTipParameter parameter)
    {
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle;
        BrandCoreId = parameter.BrandCoreId;
        VehicleTypeCoreId = parameter.VehicleTypeCoreId;
        VehicleSystemCoreId = parameter.VehicleSystemCoreId;
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

    public MoveDirection GetMoveDirection(Common.ValueObjects.Priority newPriority)
    {
        if (newPriority > Priority)
            return MoveDirection.Down;
        if (newPriority < Priority)
            return MoveDirection.Up;
        return MoveDirection.NoChange;
    }
}
