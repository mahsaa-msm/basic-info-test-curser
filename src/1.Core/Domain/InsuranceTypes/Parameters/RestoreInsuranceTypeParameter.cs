using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Master.Data.Core.Domain.InsuranceTypes.Parameters;
public sealed record RestoreInsuranceTypeParameter(Title Title,
                                             NullableTitle DisplayTitle,
                                             Code Code,
                                             Common.ValueObjects.Priority Priority);
