using Zamin.Core.RequestResponse.Commands;

namespace Master.Data.Core.RequestResponse.TravelDurationTypess.Create;

public sealed class CreateTravelDurationTypesCommand : ICommand<Guid>
{
    public string Title { get; set; } = string.Empty;
    public int CoreId { get; set; }
}
