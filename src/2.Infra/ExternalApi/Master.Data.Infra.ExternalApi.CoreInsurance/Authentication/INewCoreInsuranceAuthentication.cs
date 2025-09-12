using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Master.Data.Infra.ExternalApi.CoreInsurance.Authentication;

public interface INewCoreInsuranceAuthentication : ITransientLifetime
{
    Task<string> GetToken();
}