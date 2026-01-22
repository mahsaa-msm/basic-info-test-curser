using Master.Data.Core.Domain.AgreementObligations.Parameters;
using Master.Data.Core.Domain.Common.Entities;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Resources;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.AgreementObligations.Entities;

public sealed class AgreementObligation : BaseTenantEntity
{
    #region Properties
    public DIPTitle Title { get; private set; }
    public DIPTitle DisplayTitle { get; private set; }
    public CoreId CoreId { get; private set; }
    public CoreId AgreementCoreId { get; private set; }
    public Code Code { get; private set; }
    public DateTime? StartDateUtc { get; private set; }
    public DateTime? EndDateUtc { get; private set; }
    public Percentage? PrepaymentPercentage { get; private set; }
    /// <summary>
    /// per days
    /// </summary>
    public int? FirstInstallmentDeadline { get; private set; }
    public int? InstallmentsCount { get; private set; }
    /// <summary>
    /// per mounth
    /// </summary>
    public int? InstallmentInterval { get; private set; }
    public string AgreementObligationNumber { get; private set; }
    public string AgreementNumber { get; private set; }
    public CoreId InsuranceTypeCoreId { get; private set; }
    public SalesType SalesType { get; private set; }

    public Common.ValueObjects.Priority Priority { get; private set; }
    public IsActive IsActive { get; private set; }

    private HashSet<CoreId> _issuanceSchemeCoreIds { get; set; } = new();
    public IReadOnlyCollection<CoreId> IssuanceSchemeCoreIds => _issuanceSchemeCoreIds.ToList().AsReadOnly();
    #endregion

    #region Constructors
    private AgreementObligation()
    {

    }

    private AgreementObligation(CreateAgreementObligationParameter createAgreementObligationParameter)
    {
        Title = createAgreementObligationParameter.Title;
        DisplayTitle = createAgreementObligationParameter.DisplayTitle.IsNull ?
                                createAgreementObligationParameter.Title.Value :
                                createAgreementObligationParameter.DisplayTitle.Value;
        CoreId = createAgreementObligationParameter.CoreId;
        AgreementCoreId = createAgreementObligationParameter.AgreementCoreId;
        Code = createAgreementObligationParameter.Code;
        StartDateUtc = createAgreementObligationParameter.StartDateUtc;
        EndDateUtc = createAgreementObligationParameter.EndDateUtc;
        PrepaymentPercentage = createAgreementObligationParameter.PrepaymentPercentage;
        FirstInstallmentDeadline = createAgreementObligationParameter.FirstInstallmentDeadline;
        InstallmentsCount = createAgreementObligationParameter.InstallmentsCount;
        InstallmentInterval = createAgreementObligationParameter.InstallmentInterval;
        AgreementObligationNumber = createAgreementObligationParameter.AgreementObligationNumber;
        AgreementNumber = createAgreementObligationParameter.AgreementNumber;
        InsuranceTypeCoreId = createAgreementObligationParameter.InsuranceTypeCoreId;
        SalesType = createAgreementObligationParameter.SalesType;
        Priority = createAgreementObligationParameter.Priority;
        if (createAgreementObligationParameter.TenantId is not null)
            TenantId = (long)createAgreementObligationParameter.TenantId;
        if (createAgreementObligationParameter.TenantKey is not null)
            TenantBusinessId = createAgreementObligationParameter.TenantKey;
        IsActive = IsActive.True();
    }
    #endregion

    #region Commands
    public static AgreementObligation Create(CreateAgreementObligationParameter createAgreementObligationParameter)
        => new(createAgreementObligationParameter);

    public static AgreementObligation CreateWithTenantId(CreateAgreementObligationParameter createAgreementObligationParameter)
    {
        if (createAgreementObligationParameter.TenantId is null)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_REQUIRED,
                                                  ProjectTranslation.TENANT_ID);

        return new(createAgreementObligationParameter);
    }

    public void Update(UpdateAgreementObligationParameter updateAgreementObligationParameter)
    {
        Title = updateAgreementObligationParameter.Title;
        DisplayTitle = updateAgreementObligationParameter.DisplayTitle.Value;
        AgreementCoreId = updateAgreementObligationParameter.AgreementCoreId;
        Code = updateAgreementObligationParameter.Code;
        StartDateUtc = updateAgreementObligationParameter.StartDateUtc;
        EndDateUtc = updateAgreementObligationParameter.EndDateUtc;
        PrepaymentPercentage = updateAgreementObligationParameter.PrepaymentPercentage;
        FirstInstallmentDeadline = updateAgreementObligationParameter.FirstInstallmentDeadline;
        InstallmentsCount = updateAgreementObligationParameter.InstallmentsCount;
        InstallmentInterval = updateAgreementObligationParameter.InstallmentInterval;
        AgreementObligationNumber = updateAgreementObligationParameter.AgreementObligationNumber;
        AgreementNumber = updateAgreementObligationParameter.AgreementNumber;
        InsuranceTypeCoreId = updateAgreementObligationParameter.InsuranceTypeCoreId;
        SalesType = updateAgreementObligationParameter.SalesType;
        Priority = updateAgreementObligationParameter.Priority;
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

    public void AddIssuanceScheme(CoreId issuanceSchemeCoreId)
    {
        if (!_issuanceSchemeCoreIds.Add(issuanceSchemeCoreId))
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                  ProjectTranslation.ISSUANCE_SCHEME);
    }

    public void RemoveIssuanceScheme(CoreId issuanceSchemeCoreId)
    {
        if (!_issuanceSchemeCoreIds.Remove(issuanceSchemeCoreId))
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST,
                                                  ProjectTranslation.ISSUANCE_SCHEME);
    }

    public void UpdateIssuanceSchemes(List<CoreId> issuanceSchemeCoreIds)
    {
        _issuanceSchemeCoreIds.RemoveWhere(_issuanceSchemeCoreId => !issuanceSchemeCoreIds.Contains(_issuanceSchemeCoreId));
        issuanceSchemeCoreIds.ToHashSet().RemoveWhere(_issuanceSchemeCoreIds.Contains);
        foreach (var issuanceSchemeCoreId in issuanceSchemeCoreIds)
        {
            _issuanceSchemeCoreIds.Add(issuanceSchemeCoreId);
        }
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
