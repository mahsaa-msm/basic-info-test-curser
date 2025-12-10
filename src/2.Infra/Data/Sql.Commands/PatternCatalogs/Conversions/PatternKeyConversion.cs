using Master.Data.Core.Domain.PatternCatalogs.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Master.Data.Infra.Data.Sql.Commands.PatternCatalogs.Conversions;

public sealed class PatternKeyConversion : ValueConverter<PatternKey, string>
{
    public PatternKeyConversion() : base(patternKey => patternKey.Value, value => PatternKey.FromString(value)) { }
}