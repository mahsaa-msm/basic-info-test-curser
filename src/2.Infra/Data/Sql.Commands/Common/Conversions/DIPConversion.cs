using Master.Data.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Master.Data.Infra.Data.Sql.Commands.Common.Conversions;

public sealed class DIPConversion : ValueConverter<DIPTitle, string>
{
    public DIPConversion() : base(name => name.Value, value => DIPTitle.FromString(value)) { }
}