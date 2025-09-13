using Zamin.Core.RequestResponse.Commands;

namespace Master.Data.Core.RequestResponse.TravelPassengerCountTypes.Create;

public sealed class CreateTravelPassengerCountTypeCommand : ICommand<Guid>
{
    public string Title { get; set; } = string.Empty;
    public int CoreId { get; set; }
}
