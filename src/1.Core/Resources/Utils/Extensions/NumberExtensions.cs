namespace Vehicle.Insurance.Core.Resources.Utils.Extensions;

public static class NumberExtensions
{
    /// <summary>
    /// تبدیل تایم استمپ (ثانیه یا میلی‌ثانیه) به DateTime
    /// </summary>
    /// <param name="timestamp">عدد تایم استمپ</param>
    /// <param name="isMilliseconds">آیا ورودی میلی‌ثانیه است؟ (پیش‌فرض: بله)</param>
    /// <param name="targetKind">نوع زمان خروجی (Utc, Local, یا Unspecified)</param>
    public static DateTime? ToSafeDateTime(this long? timestamp, bool isMilliseconds = true, DateTimeKind targetKind = DateTimeKind.Utc)
    {
        if (timestamp == null || timestamp == 0)
            return null;

        try
        {
            DateTimeOffset dateTimeOffset;

            // تشخیص و تبدیل بر اساس ثانیه یا میلی‌ثانیه
            if (isMilliseconds)
                dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp.Value);
            else
                dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp.Value);

            // تبدیل به نوع DateTime با در نظر گرفتن TimeZone مورد نظر
            switch (targetKind)
            {
                case DateTimeKind.Local:
                    return dateTimeOffset.LocalDateTime;
                case DateTimeKind.Utc:
                    return dateTimeOffset.UtcDateTime;
                case DateTimeKind.Unspecified:
                default:
                    return dateTimeOffset.DateTime;
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            // مدیریت تاریخ‌های خیلی قدیمی یا خیلی آینده که خارج از بازه DateTime هستند
            return null;
        }
    }
}

