using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Common.Conversions;

public sealed class NullableTitleConversion : ValueConverter<NullableTitle, string?>
{
    public NullableTitleConversion() : base(nullableTitle => nullableTitle.Value, value => NullableTitle.FromString(value)) { }
}
