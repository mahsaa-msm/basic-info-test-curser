using Vehicle.Insurance.Core.Domain.PatternCatalogs.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.PatternCatalogs.Conversions;

public sealed class RegexExpressionConversion : ValueConverter<RegexExpression, string>
{
    public RegexExpressionConversion() : base(regexExpression => regexExpression.Value, value => RegexExpression.FromString(value)) { }
}
