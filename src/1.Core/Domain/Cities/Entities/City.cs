using Vehicle.Insurance.Core.Domain.Cities.Parameters;
using Vehicle.Insurance.Core.Domain.Common.Entities;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.Domain.Cities.Entities;

public sealed class City : BaseTenantEntity
{
    #region Properties
    public DIPTitle Title { get; private set; }
    public DIPTitle DisplayTitle { get; private set; }
    public CoreId CoreId { get; private set; }
    public CoreId ProvinceCoreId { get; private set; }
    public Code Code { get; private set; }
    public Common.ValueObjects.Priority Priority { get; private set; }
    public IsActive IsActive { get; private set; }
    public IsDeleted IsDeleted { get; private set; }
    #endregion

    #region Constructors
    private City() { }

    private City(CreateCityParameter parameter)
    {
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull ?
            parameter.Title.Value :
            parameter.DisplayTitle.Value;
        CoreId = parameter.CoreId;
        Code = parameter.Code;
        Priority = parameter.Priority;
        ProvinceCoreId = parameter.ProvinceCoreId;
        IsActive = IsActive.True();
        IsDeleted = IsDeleted.False();
    }

    private City(CreateCityWithTenantIdParameter parameter)
    {
        TenantId = parameter.TenantId;
        TenantBusinessId = parameter.TenantKey;
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull ?
            parameter.Title.Value :
            parameter.DisplayTitle.Value;
        CoreId = parameter.CoreId;
        Code = parameter.Code;
        Priority = parameter.Priority;
        ProvinceCoreId = parameter.ProvinceCoreId;
        IsActive = IsActive.True();
        IsDeleted = IsDeleted.False();
    }
    #endregion

    #region Commands
    public static City Create(CreateCityParameter parameter)
        => new(parameter);

    public static City CreateWithTenantId(CreateCityWithTenantIdParameter parameter)
    => new(parameter);

    public void Update(UpdateCityParameter parameter)
    {
        if (IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.CITY);

        Code = parameter.Code;
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle;
        Priority = parameter.Priority;
        ProvinceCoreId = parameter.ProvinceCoreId;
    }

    public void Delete()
    {
        if (IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.CITY);

        IsDeleted = IsDeleted.True();
    }

    public void Restore(RestoreCityParameter parameter)
    {
        if (!IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_CAN_NOT_RESTORE_NOT_DELETED,
                                                  ProjectTranslation.CITY);

        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull ?
            parameter.Title.Value :
            parameter.DisplayTitle.Value;
        Code = parameter.Code;
        Priority = parameter.Priority;
        ProvinceCoreId = parameter.ProvinceCoreId;
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
                                                  ProjectTranslation.CITY);

        Priority = Priority.Increase();
    }

    public void PullUp()
    {
        if (IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.CITY);

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

