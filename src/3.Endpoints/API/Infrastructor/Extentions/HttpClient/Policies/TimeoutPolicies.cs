using Master.Data.Core.Contracts.Common.Options;
using Master.Data.Core.Resources;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Timeout;

namespace Master.Data.Endpoints.API.Infrastructor.Extentions.HttpClient.Policies;

public class TimeoutPolicies
{
    public AsyncTimeoutPolicy<HttpResponseMessage> DynamicTimeoutPolicy { get; }
    public TimeoutPolicies(ILogger<TimeoutPolicies> logger, IOptions<PollyOptions> pollyOptions)
    {
        if (!Enum.TryParse<TimeoutStrategy>(pollyOptions.Value.Timeout.Strategy, true, out var timeoutStrategy))
            timeoutStrategy = TimeoutStrategy.Pessimistic;

        DynamicTimeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(
            timeout: TimeSpan.FromSeconds(pollyOptions.Value.Timeout.TimeoutSeconds),
            timeoutStrategy: timeoutStrategy,
            onTimeoutAsync: (context, timeout, task) =>
            {
                logger.LogWarning(string.Format(ProjectTranslation.TIMEOUT_REQUEST_DURATION_POLLY,
                                                context.PolicyKey,
                                                timeout.TotalNanoseconds));
                return Task.CompletedTask;
            });
    }
}
