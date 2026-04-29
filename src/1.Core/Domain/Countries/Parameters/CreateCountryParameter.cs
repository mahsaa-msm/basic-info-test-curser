using Vehicle.Insurance.Core.Domain.Common.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Countries.Parameters;

public sealed record CreateCountryParameter(DIPTitle Title,
                                            NullableTitle DisplayTitle,
                                            CoreId CoreId,
                                            Code Code,
                                            Common.ValueObjects.Priority Priority);
