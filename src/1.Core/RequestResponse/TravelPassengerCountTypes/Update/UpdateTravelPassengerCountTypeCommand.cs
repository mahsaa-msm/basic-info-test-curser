using Zamin.Core.RequestResponse.Commands;

namespace Master.Data.Core.RequestResponse.TravelPassengerCountTypes.Update;
public class UpdateTravelPassengerCountTypeCommand : ICommand
{
    public string Title { get; set; } = string.Empty;
    public int Id { get; set; }
    public int CoreId { get; set; }
    public long Priority { get; set; }
}
