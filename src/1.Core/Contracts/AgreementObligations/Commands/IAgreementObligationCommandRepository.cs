using Master.Data.Core.Domain.AgreementObligations.Entities;
using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Contracts.Data.Commands;

namespace Master.Data.Core.Contracts.AgreementObligations.Commands;

public interface IAgreementObligationCommandRepository : ICommandRepository<AgreementObligation, long>
{
    Task<AgreementObligation?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId);
    Task<List<AgreementObligation>> GetByIds(List<long> agreementObligationIds);
    Task<List<AgreementObligation>> GetByTenantId(long tenantId);
    Task<List<AgreementObligation>> GetAllAsync();
    Task<List<AgreementObligation>> GetAllIgnoreQueryFilterAsync();
    Task<List<AgreementObligation>> GetSubordinateAgreementObligations(Priority current, Priority @new);
    Task<List<AgreementObligation>> GetSubordinateAgreementObligations(Priority current);
    Task<List<AgreementObligation>> GetSuperiorAgreementObligations(Priority current, Priority @new);
    Task<long> GetNextPriority();
    bool IsCreatedByCore(AgreementObligation agreementObligation);
}