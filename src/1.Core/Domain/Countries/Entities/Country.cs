using Master.Data.Core.Domain.Common.Entities;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.Countries.Parameters;
using Master.Data.Core.Resources;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.Countries.Entities;
public sealed class Country : BaseTenantEntity
{
    #region Properties
    public Title Title { get; private set; }
    public Title DisplayTitle { get; private set; }
    public CoreId CoreId { get; private set; }
    public Code Code { get; private set; }
    public Common.ValueObjects.Priority Priority { get; private set; }
    public IsActive IsActive { get; private set; }
    public IsDeleted IsDeleted { get; private set; }
    #endregion

    #region Constructors
    private Country() { }

    private Country(CreateCountryParameter parameter)
    {
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull ?
            parameter.Title.Value :
            parameter.DisplayTitle.Value;
        CoreId = parameter.CoreId;
        Code = parameter.Code;
        Priority = parameter.Priority;
        IsActive = IsActive.True();
        IsDeleted = IsDeleted.False();
    }
    #endregion

    #region Commands
    public static Country Create(CreateCountryParameter parameter)
        => new(parameter);

    public void Update(UpdateCountryParameter parameter)
    {
        if (IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.COUNTRY);

        Code = parameter.Code;
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle;
        Priority = parameter.Priority;
    }

    public void Delete()
    {
        if (IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.COUNTRY);

        IsDeleted = IsDeleted.True();
    }

    public void Restore(RestoreCountryParameter parameter)
    {
        if (!IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_CAN_NOT_RESTORE_NOT_DELETED,
                                                  ProjectTranslation.COUNTRY);

        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull ?
            parameter.Title.Value :
            parameter.DisplayTitle.Value;
        Code = parameter.Code;
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
                                                  ProjectTranslation.COUNTRY);

        Priority = Priority.Increase();
    }

    public void PullUp()
    {
        if (IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.COUNTRY);

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
