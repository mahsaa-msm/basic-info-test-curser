using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Common.Conversions;

public sealed class PriorityConversion : ValueConverter<Priority, long>
{
    public PriorityConversion() : base(priority => priority.Value, value => Priority.FromLong(value)) { }
}
