using Master.Data.Core.Domain.Common.ValueObjects;

namespace Master.Data.Core.Domain.Countries.Parameters;

public sealed record UpdateCountryParameter(DIPTitle Title,
                                            DIPTitle DisplayTitle,
                                            Code Code,
                                            Common.ValueObjects.Priority Priority);