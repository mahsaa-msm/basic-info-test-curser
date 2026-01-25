using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.ValueObjects;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.AgreementObligations.Parameters;

public sealed record CreateAgreementObligationParameter(DIPTitle Title,
                                                        NullableTitle DisplayTitle,
                                                        CoreId CoreId,
                                                        CoreId AgreementCoreId,
                                                        Code Code,
                                                        DateTime? StartDateUtc,
                                                        DateTime? EndDateUtc,
                                                        Percentage? PrepaymentPercentage,
                                                        int? FirstInstallmentDeadline,
                                                        int? InstallmentsCount,
                                                        int? InstallmentInterval,
                                                        string AgreementObligationNumber,
                                                        string AgreementNumber,
                                                        CoreId InsuranceTypeCoreId,
                                                        SalesType SalesType,
                                                        Common.ValueObjects.Priority Priority,
                                                        long? TenantId = null,
                                                        BusinessId? TenantKey = null);
