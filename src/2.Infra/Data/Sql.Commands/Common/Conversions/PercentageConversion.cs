using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Common.Conversions;

public sealed class PercentageConversion : ValueConverter<Percentage, double>
{
    public PercentageConversion() : base(percentage => percentage.Value, value => Percentage.FromDouble(value)) { }
}

