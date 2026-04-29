using Vehicle.Insurance.Core.Domain.PatternCatalogs.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.PatternCatalogs.Conversions;

public sealed class PatternKeyConversion : ValueConverter<PatternKey, string>
{
    public PatternKeyConversion() : base(patternKey => patternKey.Value, value => PatternKey.FromString(value)) { }
}
