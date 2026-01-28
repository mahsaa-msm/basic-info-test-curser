using Master.Data.Core.Domain.Common.ValueObjects;

namespace Master.Data.Core.Domain.Cities.Parameters;

public sealed record UpdateCityParameter(DIPTitle Title,
                                         DIPTitle DisplayTitle,
                                         Code Code,
                                         Common.ValueObjects.Priority Priority,
                                         CoreId ProvinceCoreId);
