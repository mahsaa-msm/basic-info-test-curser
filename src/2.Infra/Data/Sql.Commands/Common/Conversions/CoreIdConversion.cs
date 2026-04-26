using Master.Data.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Master.Data.Infra.Data.Sql.Commands.Common.Conversions;

public sealed class CoreIdConversion : ValueConverter<CoreId, string>
{
    public CoreIdConversion() : base(coreId => coreId.Value, value => CoreId.FromString(value)) { }
}
