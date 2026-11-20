namespace TripWise.Application.Features.TripDestinations.Queries.GetTripDestinations;

public sealed record TripDestinationListItemDto(
    Guid Id,
    Guid DestinationId,
    string CityName,
    string CountryName,
    string CountryCode,
    int VisitOrder,
    DateOnly ArrivalDate,
    DateOnly DepartureDate);