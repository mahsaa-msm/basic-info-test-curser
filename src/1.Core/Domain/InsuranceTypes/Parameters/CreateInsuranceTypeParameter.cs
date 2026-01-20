using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Master.Data.Core.Domain.InsuranceTypes.Parameters;
public sealed record CreateInsuranceTypeParameter(Title Title,
                                            NullableTitle DisplayTitle,
                                            CoreId CoreId,
                                            Code Code,
                                            Common.ValueObjects.Priority Priority);