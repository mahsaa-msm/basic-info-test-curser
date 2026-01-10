using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.Cities.Commands.Delete;
public sealed class DeleteCityCommand : ICommand, IWebRequest
{
    public long CityId { get; set; }

    public string Path => "/Api/City/DeleteCity";
}