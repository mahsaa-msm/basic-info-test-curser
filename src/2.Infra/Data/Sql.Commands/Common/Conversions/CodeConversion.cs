using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Common.Conversions;

public sealed class CodeConversion : ValueConverter<Code, string>
{
    public CodeConversion() : base(code => code.Value, value => Code.FromString(value)) { }
}
