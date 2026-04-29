using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.AgreementObligations.Conversions;

public sealed class IssuanceSchemeCoreIdsConversion : ValueConverter<List<string>, string>
{
    public IssuanceSchemeCoreIdsConversion() : base(
        coreIds => string.Join(",", coreIds.Select(coreId => coreId.ToString())),
        value => string.IsNullOrEmpty(value) ?
                      new List<string>() :
                      value.Split(',', StringSplitOptions.RemoveEmptyEntries)
                           .Select(id => id)
                           .ToList())
    { }
}


