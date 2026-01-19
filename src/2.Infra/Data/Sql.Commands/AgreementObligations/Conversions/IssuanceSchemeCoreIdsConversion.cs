using Master.Data.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Master.Data.Infra.Data.Sql.Commands.AgreementObligations.Conversions;

public sealed class IssuanceSchemeCoreIdsConversion : ValueConverter<List<CoreId>, string>
{
    public IssuanceSchemeCoreIdsConversion() : base(
        coreIds => string.Join(",", coreIds.Select(coreId => coreId.ToString())),
        value => string.IsNullOrEmpty(value) ?
                      new List<CoreId>() :
                      value.Split(',', StringSplitOptions.RemoveEmptyEntries)
                           .Select(id => (CoreId)id)
                           .ToList())
    { }
}

public static class IssuanceSchemeCoreIdsComparer
{
    public static ValueComparer<List<CoreId>> CoreIdListComparer = new ValueComparer<List<CoreId>>(
        (c1, c2) => c1.SequenceEqual(c2),
        c => c.Aggregate(0, (acc, id) => acc ^ id.GetHashCode()),
        c => c.ToList()
    );
}