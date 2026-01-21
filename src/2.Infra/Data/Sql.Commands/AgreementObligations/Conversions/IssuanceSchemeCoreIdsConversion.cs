using Master.Data.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Master.Data.Infra.Data.Sql.Commands.AgreementObligations.Conversions;

public sealed class IssuanceSchemeCoreIdsConversion : ValueConverter<HashSet<CoreId>, string>
{
    public IssuanceSchemeCoreIdsConversion()
            : base(
                coreIds =>
                    string.Join(",", coreIds.Select(coreId => coreId.Value)),

                value =>
                    string.IsNullOrWhiteSpace(value)
                        ? new HashSet<CoreId>()
                        : value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                               .Select(CoreId.FromString)
                               .ToHashSet()
            )
    {
    }
}

public static class IssuanceSchemeCoreIdsComparer
{
    public static readonly ValueComparer<HashSet<CoreId>> CoreIdHashSetComparer =
        new(
            (c1, c2) => c1.SetEquals(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => new HashSet<CoreId>(c)
        );
}