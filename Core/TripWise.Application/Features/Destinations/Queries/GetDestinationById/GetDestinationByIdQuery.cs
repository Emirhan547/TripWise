using MediatR;

using TripWise.Application.Common.Results;

namespace TripWise.Application.Features.Destinations.Queries.GetDestinationById;

    public sealed record GetDestinationByIdQuery(Guid Id): IRequest<Result<DestinationDetailsDto>>;

