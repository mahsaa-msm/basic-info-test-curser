using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Master.Data.Core.Domain.InsuranceTypes.Parameters;
public sealed record UpdateInsuranceTypeParameter(Title Title,
                                            Title DisplayTitle,
                                            Code Code,
                                            Common.ValueObjects.Priority Priority);