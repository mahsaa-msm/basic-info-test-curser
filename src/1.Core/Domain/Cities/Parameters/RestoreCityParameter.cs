using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Master.Data.Core.Domain.Cities.Parameters;
public sealed record RestoreCityParameter(Title Title,
                                          NullableTitle DisplayTitle,
                                          Code Code,
                                          Common.ValueObjects.Priority Priority,
                                          CoreId ProvinceCoreId);
