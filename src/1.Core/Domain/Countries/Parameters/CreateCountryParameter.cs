using Master.Data.Core.Domain.Common.ValueObjects;

namespace Master.Data.Core.Domain.Countries.Parameters;

public sealed record CreateCountryParameter(DIPTitle Title,
                                            NullableTitle DisplayTitle,
                                            CoreId CoreId,
                                            Code Code,
                                            Common.ValueObjects.Priority Priority);