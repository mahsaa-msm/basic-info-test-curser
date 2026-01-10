using Master.Data.Core.Domain.Common.Entities;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.Provinces.Parameters;
using Master.Data.Core.Resources;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.Provinces.Entities;
public sealed class Province : BaseTenantEntity
{
    #region Properties
    public Title Title { get; private set; }
    public Title DisplayTitle { get; private set; }
    public CoreId CoreId { get; private set; }
    public CoreId CountryCoreId { get; private set; }
    public Code Code { get; private set; }
    public Common.ValueObjects.Priority Priority { get; private set; }
    public IsActive IsActive { get; private set; }
    public IsDeleted IsDeleted { get; private set; }
    #endregion

    #region Constructors
    private Province() { }

    private Province(CreateProvinceParameter parameter)
    {
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull ?
            parameter.Title.Value :
            parameter.DisplayTitle.Value;
        CoreId = parameter.CoreId;
        Code = parameter.Code;
        Priority = parameter.Priority;
        CountryCoreId = parameter.CountryCoreId;
        IsActive = IsActive.True();
        IsDeleted = IsDeleted.False();
    }

    private Province(CreateProvinceWithTenantIdParameter parameter)
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
        CountryCoreId = parameter.CountryCoreId;
        IsActive = IsActive.True();
        IsDeleted = IsDeleted.False();
    }
    #endregion

    #region Commands
    public static Province Create(CreateProvinceParameter parameter)
        => new(parameter);

    public static Province CreateWithTenantId(CreateProvinceWithTenantIdParameter parameter)
    => new(parameter);

    public void Update(UpdateProvinceParameter parameter)
    {
        if (IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.PROVINCE);

        Code = parameter.Code;
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle;
        Priority = parameter.Priority;
        CountryCoreId = parameter.CountryCoreId;
    }

    public void Delete()
    {
        if (IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.PROVINCE);

        IsDeleted = IsDeleted.True();
    }

    public void Restore(RestoreProvinceParameter parameter)
    {
        if (!IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_CAN_NOT_RESTORE_NOT_DELETED,
                                                  ProjectTranslation.PROVINCE);

        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull ?
            parameter.Title.Value :
            parameter.DisplayTitle.Value;
        Code = parameter.Code;
        Priority = parameter.Priority;
        CountryCoreId = parameter.CountryCoreId;
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
                                                  ProjectTranslation.PROVINCE);

        Priority = Priority.Increase();
    }

    public void PullUp()
    {
        if (IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.PROVINCE);

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
