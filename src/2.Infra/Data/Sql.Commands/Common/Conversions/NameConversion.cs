using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Common.Conversions;

public sealed class NameConversion : ValueConverter<Name, string>
{
    public NameConversion() : base(name => name.Value, value => Name.FromString(value)) { }
}
