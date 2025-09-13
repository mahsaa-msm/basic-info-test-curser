using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Master.Data.Infra.ExternalApi.CoreInsurance.Authentication;

public interface ICoreInsuranceAuthentication : ITransientLifetime
{
    Task<string> GetToken();
}
