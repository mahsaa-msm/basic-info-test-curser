using Master.Data.Core.Domain.Common.Entities;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.TravelPassengerCountTypes.Events;
using Zamin.Core.Domain.Entities;

namespace Master.Data.Core.Domain.TravelDurationTypes.Entities;
public class TravelDurationType : BaseTenantEntity<long>
{
    public DIPTitle Title { get; private set; }
    public int CoreId { get; private set; }
    public Priority Priority { get; private set; }
    public bool IsEnable { get; private set; } = true;

    private TravelDurationType()
    {
    }
    public TravelDurationType(int coreId, DIPTitle name, Priority priority)
    {
        CoreId = coreId;
        Title = name;
        Priority = priority;
    }

    public void Update(int coreId, DIPTitle title, Priority priority)
    {
        CoreId = coreId;
        Title = title;
        Priority = priority;
        AddEvent(new TravelDurationTypesUpdated(BusinessId.Value));
    }

    public static TravelDurationType Create(int coreId, DIPTitle title, Priority priority) => new(coreId, title, priority);
    public void Enable() => IsEnable = true;
    public void Disable() => IsEnable = false;

}
