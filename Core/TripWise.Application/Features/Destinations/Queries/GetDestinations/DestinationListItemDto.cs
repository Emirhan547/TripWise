

namespace TripWise.Application.Features.Destinations.Queries.GetDestinations;

    public sealed record DestinationListItemDto(
     Guid Id,
     string CityName,
     string CountryName,
     string CountryCode,
     DateTimeOffset CreatedAt);
