using MediatR;
using TripWise.Application.Common.Models;
using TripWise.Application.Common.Results;

namespace TripWise.Application.Features.Destinations.Queries.GetDestinations;
public sealed record GetDestinationsQuery(
    int PageNumber = 1,
    int PageSize = 10)
    : IRequest<Result<PagedResult<DestinationListItemDto>>>;
