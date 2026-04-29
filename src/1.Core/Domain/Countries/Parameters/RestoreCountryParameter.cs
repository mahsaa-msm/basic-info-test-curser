using Vehicle.Insurance.Core.Domain.Common.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Countries.Parameters;

public sealed record RestoreCountryParameter(DIPTitle Title,
                                             NullableTitle DisplayTitle,
                                             Code Code,
                                             Common.ValueObjects.Priority Priority);

