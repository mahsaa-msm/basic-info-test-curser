using Vehicle.Insurance.Core.Contracts.Common.Options;
using Vehicle.Insurance.Core.Resources;
using Microsoft.Extensions.Options;
using Polly;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.Extentions.HttpClient.Policies;

public class RetryPolicies
{
    public IAsyncPolicy<HttpResponseMessage> BasicRetryPolicy { get; }

    public RetryPolicies(ILogger<RetryPolicies> logger, IOptions<PollyOptions> pollyOptions)
    {
        BasicRetryPolicy = Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(r => (int)r.StatusCode >= 500)
            .WaitAndRetryAsync(
                retryCount: pollyOptions.Value.Retry.RetryCount,
                sleepDurationProvider: (retryAttempt, response) =>
                {
                    var delay = TimeSpan.FromSeconds(
                        pollyOptions.Value.Retry.MedianFirstRetryDelaySeconds *
                        Math.Pow(pollyOptions.Value.Retry.BackoffExponent, retryAttempt - 1));

                    logger.LogInformation(string.Format(ProjectTranslation.RETRY_REQUEST_WITH_DELAY_POLLY,
                                                        retryAttempt,
                                                        delay.TotalNanoseconds));
                    return delay;
                },
                onRetry: (response, timespan, retryCount, context) =>
                {
                    logger.LogWarning(string.Format(ProjectTranslation.RETRY_REQUEST_POLLY, retryCount, context.PolicyKey));
                });
    }
}

