using MediatR;
using TripWise.Application.Common.Models;
using TripWise.Application.Common.Results;

namespace TripWise.Application.Features.Trips.Queries.GetTrips;

public sealed record GetTripsQuery(int PageNumber = 1,int PageSize = 10): IRequest<Result<PagedResult<TripListItemDto>>>;

