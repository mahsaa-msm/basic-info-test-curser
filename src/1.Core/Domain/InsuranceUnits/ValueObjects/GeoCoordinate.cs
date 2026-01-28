using Zamin.Core.Domain.ValueObjects;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.InsuranceUnits.ValueObjects;

public sealed class GeoCoordinate : BaseValueObject<GeoCoordinate>
{
    public Latitude Latitude { get; }
    public Longitude Longitude { get; }

    public GeoCoordinate(Latitude latitude, Longitude longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    /// <summary>
    /// بررسی آیا در محدوده مشخص شده قرار دارد
    /// </summary>
    public bool IsInBounds(double north, double south, double east, double west)
    {
        return Latitude.Value <= north && Latitude.Value >= south &&
               Longitude.Value <= east && Longitude.Value >= west;
    }

    /// <summary>
    /// محاسبه فاصله بین دو نقطه جغرافیایی با استفاده از فرمول Haversine
    /// </summary>
    /// <param name="other">مختصات مقصد</param>
    /// <returns>فاصله بر حسب کیلومتر</returns>
    public double CalculateDistance(GeoCoordinate other)
    {
        const double earthRadiusKm = 6371.0; // شعاع زمین بر حسب کیلومتر

        var lat1 = ToRadians(Latitude.Value);
        var lon1 = ToRadians(Longitude.Value);
        var lat2 = ToRadians(other.Latitude.Value);
        var lon2 = ToRadians(other.Longitude.Value);

        var dLat = lat2 - lat1;
        var dLon = lon2 - lon1;

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        var distance = earthRadiusKm * c;

        return Math.Round(distance, 2); // گرد کردن به ۲ رقم اعشار
    }

    /// <summary>
    /// محاسبه فاصله و برگرداندن نتیجه با واحد مشخص
    /// </summary>
    public double CalculateDistance(GeoCoordinate other, DistanceUnit unit)
    {
        var distanceKm = CalculateDistance(other);

        return unit switch
        {
            DistanceUnit.Kilometers => distanceKm,
            DistanceUnit.Meters => distanceKm * 1000,
            DistanceUnit.Miles => distanceKm * 0.621371,
            _ => distanceKm
        };
    }

    /// <summary>
    /// بررسی آیا نقطه در شعاع مشخصی قرار دارد
    /// </summary>
    public bool IsWithinRadius(GeoCoordinate other, double radiusKm)
    {
        return CalculateDistance(other) <= radiusKm;
    }

    private static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }
}