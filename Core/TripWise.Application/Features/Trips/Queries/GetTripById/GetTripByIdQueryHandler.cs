using MediatR;
using TripWise.Application.Abstractions.Identity;
using TripWise.Application.Common.Results;
using TripWise.Application.Contracts.Persistence;

namespace TripWise.Application.Features.Trips.Queries.GetTripById;

public sealed class GetTripByIdQueryHandler(ITripRepository _tripRepository, ICurrentUserService currentUserService) : IRequestHandler<GetTripByIdQuery, Result<TripDetailsDto>>
{
  
    public async Task<Result<TripDetailsDto>> Handle(GetTripByIdQuery request,CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;
        var trip = await _tripRepository.GetByIdAsync(userId, request.Id,cancellationToken);

        if (trip is null)
            return Result<TripDetailsDto>.NotFound("Seyahat bulunamadı.");

        var tripDetails = new TripDetailsDto(trip.Id,
            trip.Name,
            trip.Description,
            trip.StartDate,
            trip.EndDate,
            trip.Status,
            trip.CreatedAt,
            trip.UpdatedAt);

        return Result<TripDetailsDto>.Success(tripDetails);
    }
}
