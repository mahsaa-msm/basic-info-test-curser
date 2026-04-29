using NetTopologySuite.Geometries;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.Common.Extensions;

public static class SpatialExtensions
{
    public static Polygon CreateBoundingBox(double west, double south, double east, double north)
    {
        var coordinates = new[]
        {
            new Coordinate(west, south),
            new Coordinate(east, south),
            new Coordinate(east, north),
            new Coordinate(west, north),
            new Coordinate(west, south)
        };

        return new Polygon(new LinearRing(coordinates)) { SRID = 4326 };
    }

    public static bool IsWithinBounds(this Point point, double north, double south, double east, double west)
    {
        return point.Y <= north && point.Y >= south &&
               point.X <= east && point.X >= west;
    }

    public static double? CalculateDistanceInKm(this Point point1, Point point2)
    {
        if (point1 == null || point2 == null) return null;
        return point1.Distance(point2) / 1000; // تبدیل به کیلومتر
    }
}
