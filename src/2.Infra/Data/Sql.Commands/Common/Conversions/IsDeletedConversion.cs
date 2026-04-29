using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Common.Conversions;

public sealed class IsDeletedConversion : ValueConverter<IsDeleted, bool>
{
    public IsDeletedConversion() : base(isDeleted => isDeleted.Value, value => IsDeleted.FromBoolean(value)) { }
}
