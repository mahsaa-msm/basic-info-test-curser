using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Resources;

namespace Vehicle.Insurance.Core.Domain.InsuranceTypes.Parameters;

public sealed record UpdateInsuranceTypeParameter(DIPTitle Title,
                                                  DIPTitle DisplayTitle,
                                                  Code Code,
                                                  ServiceFeatureCategory? ServiceFeatureCategory,
                                                  Common.ValueObjects.Priority Priority);
