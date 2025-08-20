using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Master.Data.Core.Domain.Countries.Parameters;
public sealed record CreateCountryParameter(Title Title,
                                            NullableTitle DisplayTitle,
                                            CoreId CoreId,
                                            Code Code,
                                            Common.ValueObjects.Priority Priority);