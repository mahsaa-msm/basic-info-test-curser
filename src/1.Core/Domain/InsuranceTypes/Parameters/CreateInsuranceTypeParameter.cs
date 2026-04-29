using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Resources;

namespace Vehicle.Insurance.Core.Domain.InsuranceTypes.Parameters;

public sealed record CreateInsuranceTypeParameter(DIPTitle Title,
                                                  NullableTitle DisplayTitle,
                                                  CoreId CoreId,
                                                  Code Code,
                                                  ServiceFeatureCategory? ServiceFeatureCategory,
                                                  Common.ValueObjects.Priority Priority);
