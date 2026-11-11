using MediatR;
using TripWise.Application.Common.Results;

namespace TripWise.Application.Features.Trips.Commands.CreateTrip;

public sealed record CreateTripCommand(
    string Name,
    string? Description,
    DateOnly StartDate,
    DateOnly EndDate
) : IRequest<Result<Guid>>;
