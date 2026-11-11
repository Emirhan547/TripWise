using MediatR;
using TripWise.Application.Common.Results;

namespace TripWise.Application.Features.Trips.Queries.GetTripById;

public sealed record GetTripByIdQuery(Guid Id): IRequest<Result<TripDetailsDto>>;
