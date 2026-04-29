using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.Domain.AgreementObligations.Parameters;

public sealed record UpdateAgreementObligationParameter(DIPTitle Title,
                                                        DIPTitle DisplayTitle,
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

