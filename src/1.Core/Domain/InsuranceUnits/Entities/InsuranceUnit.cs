using Vehicle.Insurance.Core.Domain.Common.Entities;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.InsuranceUnits.Parameters;
using Vehicle.Insurance.Core.Domain.InsuranceUnits.ValueObjects;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.Domain.InsuranceUnits.Entities;

public sealed class InsuranceUnit : BaseTenantEntity
{
    #region Properties
    public DIPTitle Name { get; private set; }
    public DIPTitle Title { get; private set; }
    public DIPTitle DisplayTitle { get; private set; }
    public CoreId CoreId { get; private set; }
    public CoreId CityCoreId { get; private set; }
    public Code Code { get; private set; }
    public GeoCoordinate? Location { get; private set; }
    public InsuranceUnitType Type { get; private set; }
    public InsuranceUnitState State { get; private set; }
    public Common.ValueObjects.Priority Priority { get; private set; }
    public IsActive IsActive { get; private set; }
    public IsDeleted IsDeleted { get; private set; }
    #endregion

    #region Constructors
    private InsuranceUnit() { }

    private InsuranceUnit(CreateInsuranceUnitParameter parameter)
    {
        Name = parameter.Name;
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull ?
            parameter.Title.Value :
            parameter.DisplayTitle.Value;
        CoreId = parameter.CoreId;
        CityCoreId = parameter.CityCoreId;
        Code = parameter.Code;
        Location = parameter.Location;
        Type = parameter.Type;
        State = parameter.State;
        Priority = parameter.Priority;
        IsActive = IsActive.True();
        IsDeleted = IsDeleted.False();
    }

    private InsuranceUnit(CreateInsuranceUnitWithTenantIdParameter parameter)
    {
        TenantId = parameter.TenantId;
        TenantBusinessId = parameter.TenantKey;
        Name = parameter.Name;
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull ?
            parameter.Title.Value :
            parameter.DisplayTitle.Value;
        CoreId = parameter.CoreId;
        Code = parameter.Code;
        Location = parameter.Location;
        Type = parameter.Type;
        Priority = parameter.Priority;
        IsActive = IsActive.True();
        IsDeleted = IsDeleted.False();
    }
    #endregion

    #region Commands
    public static InsuranceUnit Create(CreateInsuranceUnitParameter parameter)
        => new(parameter);

    public static InsuranceUnit CreateWithTenantId(CreateInsuranceUnitWithTenantIdParameter parameter)
    => new(parameter);

    public void Update(UpdateInsuranceUnitParameter parameter)
    {
        if (IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.INSURANCE_UNIT);

        Name = parameter.Name;
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle;
        CityCoreId = parameter.CityCoreId;
        Code = parameter.Code;
        Location = parameter.Location;
        Type = parameter.Type;
        State = parameter.State;
        Priority = parameter.Priority;
    }

    public void Delete()
    {
        if (IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.INSURANCE_UNIT);

        IsDeleted = IsDeleted.True();
    }

    public void Restore(RestoreInsuranceUnitParameter parameter)
    {
        if (!IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_CAN_NOT_RESTORE_NOT_DELETED,
                                                  ProjectTranslation.INSURANCE_UNIT);

        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull ?
            parameter.Title.Value :
            parameter.DisplayTitle.Value;
        Code = parameter.Code;
        CityCoreId = parameter.CityCoreId;
        Location = parameter.Location;
        Type = parameter.Type;
        State = parameter.State;
        Priority = parameter.Priority;
        IsActive = IsActive.True();
        IsDeleted = IsDeleted.False();
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
        if (IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.INSURANCE_UNIT);

        Priority = Priority.Increase();
    }

    public void PullUp()
    {
        if (IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.INSURANCE_UNIT);

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

