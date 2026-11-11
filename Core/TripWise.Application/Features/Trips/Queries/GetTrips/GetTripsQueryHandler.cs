using MediatR;
using TripWise.Application.Common.Models;
using TripWise.Application.Common.Results;
using TripWise.Application.Contracts.Persistence;

namespace TripWise.Application.Features.Trips.Queries.GetTrips;

public sealed class GetTripsQueryHandler(ITripRepository _tripRepository): IRequestHandler<GetTripsQuery, Result<PagedResult<TripListItemDto>>>
{
    public async Task<Result<PagedResult<TripListItemDto>>> Handle(GetTripsQuery request,CancellationToken cancellationToken)
    {
        var trips = await _tripRepository.GetAllAsync(request.PageNumber,request.PageSize,cancellationToken);
        var totalCount = await _tripRepository.GetTotalCountAsync( cancellationToken);

        var tripList = trips.Select(trip => new TripListItemDto(trip.Id,trip.Name,trip.StartDate,trip.EndDate,trip.Status, trip.CreatedAt)).ToList();

        var pagedResult = new PagedResult<TripListItemDto>(tripList,request.PageNumber,request.PageSize,totalCount);

        return Result<PagedResult<TripListItemDto>>.Success(pagedResult);
    }
}