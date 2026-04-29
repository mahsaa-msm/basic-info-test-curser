using Vehicle.Insurance.Core.Domain.Common.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Countries.Parameters;

public sealed record UpdateCountryParameter(DIPTitle Title,
                                            DIPTitle DisplayTitle,
                                            Code Code,
                                            Common.ValueObjects.Priority Priority);
