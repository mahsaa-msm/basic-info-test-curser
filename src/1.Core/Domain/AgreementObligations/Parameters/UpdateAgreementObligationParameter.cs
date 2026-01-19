using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.AgreementObligations.Parameters;

public sealed record UpdateAgreementObligationParameter(Title Title,
                                                        Title DisplayTitle,
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
                                                        Common.ValueObjects.Priority Priority);
