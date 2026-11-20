using MediatR;
using TripWise.Application.Common.Results;

namespace TripWise.Application.Features.TripDestinations.Queries.GetTripDestinations;

public sealed record GetTripDestinationsQuery(Guid TripId)
    : IRequest<Result<List<TripDestinationListItemDto>>>;