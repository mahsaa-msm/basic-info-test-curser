using Master.Data.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Master.Data.Infra.Data.Sql.Commands.Common.Conversions;

public sealed class NullableCoreIdConversion : ValueConverter<NullableCoreId, string?>
{
    public NullableCoreIdConversion() : base(coreId => coreId.Value, value => NullableCoreId.FromString(value)) { }
}