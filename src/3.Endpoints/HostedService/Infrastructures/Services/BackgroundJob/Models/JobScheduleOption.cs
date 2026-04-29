using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Constants;

namespace Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;

public sealed class JobScheduleOption
{
    public List<JobOption> Jobs { get; set; } = new();
}

public sealed class JobOption
{
    public string Name { get; set; } = string.Empty;
    public ScheduleType ScheduleType { get; set; }
    public bool IsEnabled { get; set; } = true;

    // Simple Schedule Properties
    public int? Hour { get; set; }
    public int? Minute { get; set; }
    public int? Period { get; set; } // in minutes
    public bool StartImmediately { get; set; }

    // Advanced Schedule Properties
    public List<TimeRestriction> TimeRestrictions { get; set; } = new();
    public List<DayOfWeek> AllowedDays { get; set; } = new();
    public DateRange? ActiveDateRange { get; set; }

    // Cron Schedule Properties
    public List<string> CronExpressions { get; set; } = new();

    // برای سازگاری با نسخه قبلی
    [Obsolete("Use CronExpressions instead for multiple cron support")]
    public string? CronExpression
    {
        get => CronExpressions.FirstOrDefault();
        set
        {
            if (!string.IsNullOrEmpty(value) && !CronExpressions.Contains(value))
                CronExpressions.Add(value);
        }
    }
}

public sealed class TimeRestriction
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public List<DateTime> ExcludedDates { get; set; } = new();
}

public sealed class DateRange
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

