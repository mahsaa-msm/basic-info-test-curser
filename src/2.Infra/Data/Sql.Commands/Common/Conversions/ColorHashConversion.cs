using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Common.Conversions;

public sealed class ColorHashConversion : ValueConverter<ColorHash, string>
{
    public ColorHashConversion() : base(colorHash => colorHash.Value, value => ColorHash.FromString(value)) { }
}
