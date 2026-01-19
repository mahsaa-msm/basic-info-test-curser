using Master.Data.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Master.Data.Infra.Data.Sql.Queries.AgreementObligations.Conversions;

public sealed class IssuanceSchemeCoreIdsConversion : ValueConverter<HashSet<CoreId>, string>
{
    public IssuanceSchemeCoreIdsConversion() : base(
        coreIds => string.Join(",", coreIds.Select(coreId => coreId.ToString())),
        value => new HashSet<CoreId>(string.IsNullOrEmpty(value) ?
                                        Array.Empty<CoreId>() :
                                        value.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(id => (CoreId)id)))
    { }
}
