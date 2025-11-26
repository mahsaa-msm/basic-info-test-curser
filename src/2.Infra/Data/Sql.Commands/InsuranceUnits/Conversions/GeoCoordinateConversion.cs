using Master.Data.Core.Domain.InsuranceUnits.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

namespace Master.Data.Infra.Data.Sql.Commands.InsuranceUnits.Conversions;

public sealed class GeoCoordinateConversion : ValueConverter<GeoCoordinate, Point>
{
    public GeoCoordinateConversion()
        : base(domainLocation => DomainToPoint(domainLocation),
               dbPoint => PointToDomain(dbPoint))
    {

    }

    private static Point DomainToPoint(GeoCoordinate? domainLocation)
    {
        if (domainLocation == null) return null;

        return new Point(domainLocation.Longitude.Value,
                         domainLocation.Latitude.Value)
        { SRID = 4326 };
    }

    private static GeoCoordinate PointToDomain(Point? dbPoint)
    {
        if (dbPoint == null) return null;

        return new GeoCoordinate(new Latitude(dbPoint.Y),
                                 new Longitude(dbPoint.X));
    }
}