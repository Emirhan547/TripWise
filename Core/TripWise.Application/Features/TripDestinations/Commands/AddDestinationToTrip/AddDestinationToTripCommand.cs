using System.Text.Json.Serialization;
using MediatR;
using TripWise.Application.Common.Results;

namespace TripWise.Application.Features.TripDestinations.Commands.AddDestinationToTrip;

public sealed record AddDestinationToTripCommand(
    Guid DestinationId,
    DateOnly ArrivalDate,
    DateOnly DepartureDate
) : IRequest<Result<Guid>>
{
    [JsonIgnore]
    public Guid TripId { get; init; }
}