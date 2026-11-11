using TripWise.Domain.Enum;

namespace TripWise.Application.Features.Trips.Queries.GetTrips;

public sealed record TripListItemDto(
Guid Id,
string Name,
DateOnly StartDate,
DateOnly EndDate,
TripStatus Status,
DateTimeOffset CreatedAt);
