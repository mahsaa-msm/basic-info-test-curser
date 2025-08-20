using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Master.Data.Core.Domain.Countries.Parameters;
public sealed record UpdateCountryParameter(Title Title,
                                            Title DisplayTitle,
                                            Code Code,
                                            Common.ValueObjects.Priority Priority);