using Master.Data.Core.Contracts.Common.Options;
using Master.Data.Core.Resources;
using Microsoft.Extensions.Options;
using Polly;

namespace Master.Data.Endpoints.HostedService.Infrastructures.Extensions.HttpClient.Policies;

public class CircuitBreakerPolicies
{
    public IAsyncPolicy<HttpResponseMessage> BasicCircuitBreakerPolicy { get; }

    public CircuitBreakerPolicies(ILogger<CircuitBreakerPolicies> logger, IOptions<PollyOptions> pollyOptions)
    {
        BasicCircuitBreakerPolicy = Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(r => (int)r.StatusCode >= 500)
            .AdvancedCircuitBreakerAsync(
                failureThreshold: pollyOptions.Value.CircuitBreaker.FailureThreshold,
                samplingDuration: TimeSpan.FromSeconds(30),
                minimumThroughput: 10,
                durationOfBreak: TimeSpan.FromSeconds(pollyOptions.Value.CircuitBreaker.DurationOfBreakSeconds),
                onBreak: (result, duration, context) =>
                {
                    logger.LogError(string.Format(ProjectTranslation.CERCUIT_BROKEN_POLLY,
                                                  duration.TotalSeconds));
                },
                onReset: context =>
                {
                    logger.LogInformation(ProjectTranslation.CERCUIT_RESET_POLLY);
                });
    }
}
