using Master.Data.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Master.Data.Infra.Data.Sql.Commands.Common.Conversions;

public sealed class NullablePercentageConversion : ValueConverter<NullablePercentage, double?>
{
    public NullablePercentageConversion() : base(percentage => percentage.Value, value => NullablePercentage.FromDouble(value)) { }
}
