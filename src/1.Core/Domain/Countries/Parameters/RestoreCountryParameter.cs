using Master.Data.Core.Domain.Common.ValueObjects;

namespace Master.Data.Core.Domain.Countries.Parameters;

public sealed record RestoreCountryParameter(DIPTitle Title,
                                             NullableTitle DisplayTitle,
                                             Code Code,
                                             Common.ValueObjects.Priority Priority);
