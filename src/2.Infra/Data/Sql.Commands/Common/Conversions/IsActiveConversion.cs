using Master.Data.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Master.Data.Infra.Data.Sql.Commands.Common.Conversions;

public sealed class IsActiveConversion : ValueConverter<IsActive, bool>
{
    public IsActiveConversion() : base(isActive => isActive.Value, value => IsActive.FromBoolean(value)) { }
}