

namespace TripWise.Application.Features.Destinations.Queries.GetDestinationById;

public sealed record DestinationDetailsDto(
 Guid Id,
 string CityName,
 string CountryName,
 string CountryCode,
 DateTimeOffset CreatedAt);
