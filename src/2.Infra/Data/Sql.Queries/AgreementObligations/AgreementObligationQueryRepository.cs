using Master.Data.Core.Contracts.AgreementObligations.Queries;
using Master.Data.Core.RequestResponse.AgreementObligations.Queries.GetAll;
using Master.Data.Core.RequestResponse.AgreementObligations.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.AgreementObligations.Queries.GetById;
using Master.Data.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.Utilities.Extensions;

namespace Master.Data.Infra.Data.Sql.Queries.AgreementObligations;

public sealed class AgreementObligationQueryRepository : BaseQueryRepository<MasterDataQueryDbContext>,
    IAgreementObligationQueryRepository
{
    public AgreementObligationQueryRepository(MasterDataQueryDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<AgreementObligationQr?> ExecuteAsync(GetAgreementObligationByIdQuery query)
         => await _dbContext.AgreementObligations
                .Select(c => new AgreementObligationQr
                {
                    Id = c.Id,
                    CoreId = c.CoreId,
                    Title = c.Title,
                    DisplayTitle = c.DisplayTitle,
                    Code = c.Code,
                    FirstInstallmentDeadline = c.FirstInstallmentDeadline,
                    InstallmentInterval = c.InstallmentInterval,
                    InstallmentsCount = c.InstallmentsCount,
                    PrepaymentPercentage = c.PrepaymentPercentage,
                    StartDateUtc = c.StartDateUtc,
                    EndDateUtc = c.EndDateUtc,
                    SalesType = c.SalesType,
                    AgreementNumber = c.AgreementNumber,
                    AgreementObligatoinNumber = c.AgreementObligationNumber,
                    AgreementCoreId = c.AgreementCoreId,
                    InsuranceTypeCoreId = c.InsuranceTypeCoreId,
                    Priority = c.Priority,
                    IsActive = c.IsActive,
                    IsEditable = !string.IsNullOrEmpty(c.CreatedByUserId)

                })
                .FirstOrDefaultAsync(c => c.Id == query.AgreementObligationId);

    public async Task<List<AgreementObligationSelectItemQr>> ExecuteAsync(GetAllAgreementObligationsQuery query)
        => await _dbContext.AgreementObligations
        .Where(c => !query.IsActive.HasValue || c.IsActive == query.IsActive)
        .OrderBy(c => c.Priority)
        .Select(c => new AgreementObligationSelectItemQr
        {
            Id = c.Id,
            CoreId = c.CoreId,
            DisplayTitle = c.DisplayTitle,
            AgreementNumber = c.AgreementNumber,
            AgreementObligationNumber = c.AgreementObligationNumber,
            AgreementCoreId = c.AgreementCoreId,
            InsuranceTypeCoreId = c.InsuranceTypeCoreId,
        }).ToListAsync();

    public async Task<PagedData<AgreementObligationListItemQr>> ExecuteAsync(GetAllAgreementObligationsPagedFilterQuery query)
    {
        var filter = _dbContext.AgreementObligations.AsQueryable();

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.CoreId),
                                c => c.CoreId == query.CoreId);

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.Code),
                        c => c.Code.Contains(query.Code!));

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.Title),
                        c => c.Title.Contains(query.Title!));

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.DisplayTitle),
                        c => c.DisplayTitle.Contains(query.DisplayTitle!));

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.AgreementObligationNumber),
                        c => c.AgreementObligationNumber.Contains(query.AgreementObligationNumber!));

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.AgreementNumber),
                        c => c.AgreementNumber.Contains(query.AgreementNumber!));

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.AgreementCoreId),
                        c => c.AgreementCoreId == query.AgreementCoreId);

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.InsuranceTypeCoreId),
                        c => c.InsuranceTypeCoreId == query.InsuranceTypeCoreId);

        filter = filter.WhereIf(query.IsActive is not null,
                                c => c.IsActive == query.IsActive);

        filter = filter.WhereIf(query.Priority is not null,
                                c => c.Priority == query.Priority);

        return await filter.AsNoTracking()
            .ToPagedData(query, c => new AgreementObligationListItemQr
            {
                Id = c.Id,
                Title = c.Title,
                DisplayTitle = c.DisplayTitle,
                Code = c.Code,
                CoreId = c.CoreId,
                AgreementNumber = c.AgreementNumber,
                AgreementObligationNumber = c.AgreementObligationNumber,
                AgreementCoreId = c.AgreementCoreId,
                InsuranceTypeCoreId = c.InsuranceTypeCoreId,
                IsActive = c.IsActive,
                Priority = c.Priority,
            });
    }
}
