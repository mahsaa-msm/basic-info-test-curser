using Vehicle.Insurance.Core.Domain.Common.Entities;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.InsuranceTypes.Parameters;
using Vehicle.Insurance.Core.Resources;
using System.Reflection.PortableExecutable;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.Domain.InsuranceTypes.Entities;

public sealed class InsuranceType : BaseTenantEntity
{
    #region Properties
    public DIPTitle Title { get; private set; }
    public DIPTitle DisplayTitle { get; private set; }
    public CoreId CoreId { get; private set; }
    public Code Code { get; private set; }
    public ServiceFeatureCategory? ServiceFeatureCategory { get; private set; }
    public Common.ValueObjects.Priority Priority { get; private set; }
    public IsActive IsActive { get; private set; }
    public IsDeleted IsDeleted { get; private set; }
    #endregion

    #region Constructors
    private InsuranceType() { }

    private InsuranceType(CreateInsuranceTypeParameter parameter)
    {
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull ?
            parameter.Title.Value :
            parameter.DisplayTitle.Value;
        CoreId = parameter.CoreId;
        Code = parameter.Code;
        ServiceFeatureCategory = parameter.ServiceFeatureCategory;
        Priority = parameter.Priority;
        IsActive = IsActive.True();
        IsDeleted = IsDeleted.False();
    }

    private InsuranceType(CreateInsuranceTypeWithTenantIdParameter parameter)
    {
        TenantId = parameter.TenantId;
        TenantBusinessId = parameter.TenantKey;
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull ?
            parameter.Title.Value :
            parameter.DisplayTitle.Value;
        CoreId = parameter.CoreId;
        Code = parameter.Code;
        ServiceFeatureCategory = parameter.ServiceFeatureCategory;
        Priority = parameter.Priority;
        IsActive = IsActive.True();
        IsDeleted = IsDeleted.False();
    }
    #endregion

    #region Commands
    public static InsuranceType Create(CreateInsuranceTypeParameter parameter)
        => new(parameter);

    public static InsuranceType CreateWithTenantId(CreateInsuranceTypeWithTenantIdParameter parameter)
    => new(parameter);

    public void Update(UpdateInsuranceTypeParameter parameter)
    {
        if (IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.INSURANCE_TYPE);

        Code = parameter.Code;
        ServiceFeatureCategory = parameter.ServiceFeatureCategory;
        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle;
        Priority = parameter.Priority;
    }

    public void Delete()
    {
        if (IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.INSURANCE_TYPE);

        IsDeleted = IsDeleted.True();
    }

    public void Restore(RestoreInsuranceTypeParameter parameter)
    {
        if (!IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_CAN_NOT_RESTORE_NOT_DELETED,
                                                  ProjectTranslation.INSURANCE_TYPE);

        Title = parameter.Title;
        DisplayTitle = parameter.DisplayTitle.IsNull ?
            parameter.Title.Value :
            parameter.DisplayTitle.Value;
        Code = parameter.Code;
        ServiceFeatureCategory = parameter.ServiceFeatureCategory;
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
                                                  ProjectTranslation.INSURANCE_TYPE);

        Priority = Priority.Increase();
    }

    public void PullUp()
    {
        if (IsDeleted.Value)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.INSURANCE_TYPE);

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

