using Master.Data.Core.Domain.InsuranceUnits.Parameters;
using Master.Data.Core.Domain.InsuranceUnits.ValueObjects;
using Master.Data.Core.Resources;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.RequestResponse.InsuranceUnits.Commands.Create;

public sealed class CreateInsuranceUnitCommand : ICommand<long>, IWebRequest
{
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? DisplayTitle { get; set; }
    public string Code { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string CityCoreId { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public InsuranceUnitType Type { get; set; }
    public InsuranceUnitState State { get; set; }

    public CreateInsuranceUnitParameter ToCreateParameter(long priority)
    {
        var coordinate = CreateGeoCoordinate();

        return new CreateInsuranceUnitParameter(Name,
                                                Title,
                                                DisplayTitle,
                                                CoreId,
                                                CityCoreId,
                                                Code,
                                                coordinate,
                                                Type,
                                                State,
                                                priority);
    }

    public RestoreInsuranceUnitParameter ToRestoreParameter(long priority)
    {
        var coordinate = CreateGeoCoordinate();

        return new RestoreInsuranceUnitParameter(Name,
                                                 Title,
                                                 DisplayTitle,
                                                 CityCoreId,
                                                 Code,
                                                 coordinate,
                                                 Type,
                                                 State,
                                                 priority);
    }

    private GeoCoordinate? CreateGeoCoordinate()
    {
        if (Latitude.HasValue && Longitude.HasValue)
        {
            return new GeoCoordinate(
                new Latitude(Latitude.Value),
                new Longitude(Longitude.Value)
            );
        }

        if (Latitude.HasValue != Longitude.HasValue)
            throw new InvalidOperationException(ProjectValidationError.VALIDATION_ERROR_BOTH_LATITUDE_LONGITUDE_MUST_EXIST);

        return null;
    }

    public string Path => "/Api/InsuranceUnit/CreateInsuranceUnit";
}