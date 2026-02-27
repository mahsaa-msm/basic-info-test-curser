using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Resources;

namespace Master.Data.Core.Domain.InsuranceTypes.Parameters;

public sealed record RestoreInsuranceTypeParameter(DIPTitle Title,
                                                   NullableTitle DisplayTitle,
                                                   Code Code,
                                                   ServiceFeatureCategory? ServiceFeatureCategory,
                                                   Common.ValueObjects.Priority Priority);
