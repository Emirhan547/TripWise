using MediatR;
using TripWise.Application.Common.Results;

namespace TripWise.Application.Features.TripDestinations.Commands.RemoveDestinationFromTrip;

public sealed record RemoveDestinationFromTripCommand( Guid TripId,Guid DestinationId
) : IRequest<Result<Guid>>;