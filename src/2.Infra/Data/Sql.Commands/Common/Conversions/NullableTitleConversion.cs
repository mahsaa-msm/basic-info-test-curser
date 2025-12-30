using Master.Data.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Master.Data.Infra.Data.Sql.Commands.Common.Conversions;
public sealed class NullableTitleConversion : ValueConverter<NullableTitle, string?>
{
    public NullableTitleConversion() : base(nullableTitle => nullableTitle.Value, value => NullableTitle.FromString(value)) { }
}