using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Common.Conversions;

public sealed class DIPTitleConversion : ValueConverter<DIPTitle, string>
{
    public DIPTitleConversion() : base(name => name.Value, value => DIPTitle.FromString(value)) { }
}
