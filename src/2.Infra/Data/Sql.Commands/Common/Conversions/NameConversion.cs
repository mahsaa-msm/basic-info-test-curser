using Master.Data.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Master.Data.Infra.Data.Sql.Commands.Common.Conversions;

public sealed class NameConversion : ValueConverter<Name, string>
{
    public NameConversion() : base(name => name.Value, value => Name.FromString(value)) { }
}