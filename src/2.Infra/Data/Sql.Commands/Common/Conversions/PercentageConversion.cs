using Master.Data.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Master.Data.Infra.Data.Sql.Commands.Common.Conversions;

public sealed class PercentageConversion : ValueConverter<Percentage, double>
{
    public PercentageConversion() : base(percentage => percentage.Value, value => Percentage.FromDouble(value)) { }
}
