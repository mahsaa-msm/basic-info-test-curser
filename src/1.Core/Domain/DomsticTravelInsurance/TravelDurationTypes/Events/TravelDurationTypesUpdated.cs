using Zamin.Core.Domain.Events;

namespace Master.Data.Core.Domain.TravelPassengerCountTypes.Events;

public sealed record TravelDurationTypesUpdated(Guid BusinessId) : IDomainEvent;
