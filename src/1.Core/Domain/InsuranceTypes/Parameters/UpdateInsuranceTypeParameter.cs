using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Resources;

namespace Master.Data.Core.Domain.InsuranceTypes.Parameters;

public sealed record UpdateInsuranceTypeParameter(DIPTitle Title,
                                                  DIPTitle DisplayTitle,
                                                  Code Code,
                                                  ServiceFeatureCategory? ServiceFeatureCategory,
                                                  Common.ValueObjects.Priority Priority);