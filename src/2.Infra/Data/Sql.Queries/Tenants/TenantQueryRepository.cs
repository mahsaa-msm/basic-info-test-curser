using Master.Data.Core.Contracts.Tenants.Queries;
using Master.Data.Core.RequestResponse.Tenants.Queries.GetById;
using Master.Data.Core.RequestResponse.Tenants.Queries.GetById.Dtos;
using Master.Data.Core.RequestResponse.Tenants.Queries.GetIAllSelectItem;
using Master.Data.Core.RequestResponse.Tenants.Queries.GetPagedFilter;
using Master.Data.Infra.Data.Sql.Queries.Common;
using Master.Data.Infra.Data.Sql.Queries.Tenants.Entities;
using Master.Data.Infra.Data.Sql.Queries.Tenants.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.Utilities.Extensions;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Infra.Data.Sql.Queries.Tenants;

public sealed class TenantQueryRepository : BaseQueryRepository<MasterDataQueryDbContext>, ITenantQueryRepository
{
    public TenantQueryRepository(MasterDataQueryDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<TenantGraphQr?> Execute(GetTenantByIdQuery query)
    {
        return await _dbContext.Tenants
            .IgnoreQueryFilters()
            .Where(c => c.Id == query.Id)
            .Include(c => c.Configs)
            .Select(t => new TenantGraphQr
            {
                Id = t.Id,
                TenantKey = t.TenantKey,
                Name = t.Name,
                Slug = t.Slug,
                IsActive = t.IsActive,
                CreatedDateUtc = t.CreatedDateUtc,

                Configs = t.Configs.Select(c => new
                {
                    c.Id,
                    c.ConfigType,
                    c.LastModifiedDateUtc,
                    c.Settings
                }).ToList<object>(),

                SsoConfig = t.Configs
                    .Where(c => c.ConfigType == ConfigType.SSO_CONFIG)
                    .Select(c => new SsoConfigDto
                    {
                        ConfigId = c.Id,
                        Type = c.ConfigType,
                        LastModifiedDateUtc = c.LastModifiedDateUtc,
                        SsoBasePath = (c.Settings as SsoSettings).SsoBasePath,
                        UserName = (c.Settings as SsoSettings).UserName,
                        OauthType = (c.Settings as SsoSettings).OauthType
                    })
                    .FirstOrDefault(),

                PaymentConfig = t.Configs
                    .Where(c => c.ConfigType == ConfigType.PAYMENT_CONFIG)
                    .Select(c => new PaymentConfigDto
                    {
                        ConfigId = c.Id,
                        Type = c.ConfigType,
                        LastModifiedDateUtc = c.LastModifiedDateUtc,
                        PaymentGateway = (c.Settings as PaymentSettings).PaymentGateway,
                        AllowRefund = (c.Settings as PaymentSettings).AllowRefund
                    })
                    .FirstOrDefault(),

                UIConfig = t.Configs
                    .Where(c => c.ConfigType == ConfigType.UI_STYLE_CONFIG)
                    .Select(c => new UiConfigDto
                    {
                        ConfigId = c.Id,
                        Type = c.ConfigType,
                        LastModifiedDateUtc = c.LastModifiedDateUtc,
                        Theme = (c.Settings as UiSettings).Theme
                    })
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PagedData<TenantSelectItemQr>> ExecuteAsync(GetTenantPagedFilterQuery query)
    {
        var filter = _dbContext.Tenants
            .IgnoreQueryFilters()
            .AsQueryable();
        filter = filter.WhereIf(!string.IsNullOrEmpty(query.Name), c => c.Name.Contains(query.Name!));
        filter = filter.WhereIf(!string.IsNullOrEmpty(query.Slug), c => c.Slug.Contains(query.Slug!));

        return await filter.ToPagedData(query, item => new TenantSelectItemQr
        {
            Id = item.Id,
            TenantKey = item.TenantKey,
            Name = item.Name,
            Slug = item.Slug,
            IsActive = item.IsActive,
            HasSsoConfig = item.Configs.Any(c => c.ConfigType == ConfigType.SSO_CONFIG),
            HasPaymentConfig = item.Configs.Any(c => c.ConfigType == ConfigType.PAYMENT_CONFIG),
            HasUiConfig = item.Configs.Any(c => c.ConfigType == ConfigType.UI_STYLE_CONFIG),
        });
    }

    public async Task<SsoSettings?> GetSsoSettingsAsync(long tenantId)
    {
        var config = await _dbContext
            .Set<TenantConfig>()
            .IgnoreQueryFilters()
            .Where(c => c.TenantId == tenantId && c.ConfigType == ConfigType.SSO_CONFIG)
            .FirstOrDefaultAsync();

        return config?.Settings as SsoSettings;
    }

    public async Task<PaymentSettings?> GetPaymentSettingsAsync(long tenantId)
    {
        var config = await _dbContext
            .Set<TenantConfig>()
            .IgnoreQueryFilters()
            .Where(c => c.TenantId == tenantId && c.ConfigType == ConfigType.PAYMENT_CONFIG)
            .FirstOrDefaultAsync();

        return config?.Settings as PaymentSettings;
    }

    public async Task<UiSettings?> GetUiSettingsAsync(long tenantId)
    {
        var config = await _dbContext
            .Set<TenantConfig>()
            .IgnoreQueryFilters()
            .Where(c => c.TenantId == tenantId && c.ConfigType == ConfigType.UI_STYLE_CONFIG)
            .FirstOrDefaultAsync();

        return config?.Settings as UiSettings;
    }

    public async Task<bool> ExistsAsync(Expression<Func<Tenant, bool>> expression)
        => await _dbContext.Tenants.IgnoreQueryFilters().AnyAsync(expression);

    public async Task<List<TenantIdKeyQr>> ExecuteAsync(GetAllTenantsSelectItemQuery query)
        => await _dbContext.Tenants
            .IgnoreQueryFilters()
            .Select(c => new TenantIdKeyQr
            {
                Id = c.Id,
                Key = c.TenantKey,
                Name = c.Name,
                Slug = c.Slug,
            })
            .ToListAsync();
}
