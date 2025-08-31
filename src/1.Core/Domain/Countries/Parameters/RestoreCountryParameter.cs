using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Master.Data.Core.Domain.Countries.Parameters;
public sealed record RestoreCountryParameter(Title Title,
                                             NullableTitle DisplayTitle,
                                             Code Code,
                                             Common.ValueObjects.Priority Priority);
