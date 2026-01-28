using Master.Data.Core.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Master.Data.Infra.Data.Sql.Commands.Common.Conversions;

public sealed class CodeConversion : ValueConverter<Code, string>
{
    public CodeConversion() : base(code => code.Value, value => Code.FromString(value)) { }
}