namespace Master.Data.Core.Contracts.Common.Options;
public sealed class PollyOptions
{
    public RetryPolicyOptions Retry { get; set; } = new();
    public CircuitBreakerOptions CircuitBreaker { get; set; } = new();
    public TimeoutPolicyOptions Timeout { get; set; } = new();
}

public sealed class RetryPolicyOptions
{
    /// <summary>
    /// تعداد تلاش ها (اولیه + مجدد)
    /// </summary>
    public int RetryCount { get; set; } = 3;
    /// <summary>
    /// تاخیر اولیه برای اولین تلاش مجدد
    /// </summary>
    public double MedianFirstRetryDelaySeconds { get; set; } = 1;
    /// <summary>
    /// افزایش تاخیر در هر تلاش
    /// در صورتی که MedianFirstRetryDelaySeconds برابر با 1 باشد:
    /// 1s, 2s, 4s, 8s
    /// </summary>
    public double BackoffExponent { get; set; } = 2.0;
}

public sealed class CircuitBreakerOptions
{
    /// <summary>
    /// تعداد خطا های مجاز
    /// </summary>
    public int EventsAllowedBeforeBreaking { get; set; } = 5;
    /// <summary>
    /// مدت زمان باز ماندن مدار
    /// </summary>
    public double DurationOfBreakSeconds { get; set; } = 30;
    /// <summary>
    /// درصد خطا برای بستن مدار
    /// بین 0 تا 1
    /// اگر برابر با 5 باشد، بعد از 50 درصد درخواست ها
    /// در مدت زمان باز ماندن مدار به خطا بخورند مدار بشته خواهد شد
    /// </summary>
    public double FailureThreshold { get; set; } = 0.5;
}

public sealed class TimeoutPolicyOptions
{
    /// <summary>
    /// حداکثر زمان انتظار
    /// </summary>
    public double TimeoutSeconds { get; set; } = 10;
    /// <summary>
    /// استراتژی تایم اوت
    /// Optimistic = استفاده از cancellationToken(async)
    /// Pessimistic = قطع مستقیم درخواست(sync)
    /// </summary>
    public string Strategy { get; set; } = "Pessimistic";
}
