using Master.Data.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Master.Data.Infra.Data.Sql.Commands.Common.Conversions;
public sealed class PriorityConversion : ValueConverter<Priority, long>
{
    public PriorityConversion() : base(priority => priority.Value, value => Priority.FromLong(value)) { }
}