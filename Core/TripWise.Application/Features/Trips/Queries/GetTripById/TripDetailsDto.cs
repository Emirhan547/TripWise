
using TripWise.Domain.Enum;

namespace TripWise.Application.Features.Trips.Queries.GetTripById;

public sealed record TripDetailsDto(
Guid Id,
string Name,
string Description,
DateOnly StartDate,
DateOnly EndDate,
TripStatus Status,
DateTimeOffset CreatedAt,
DateTimeOffset? UpdatedAt);
