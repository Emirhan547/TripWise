using MediatR;
using TripWise.Application.Common.Results;
using TripWise.Application.Contracts.Persistence;

namespace TripWise.Application.Features.Trips.Queries.GetTripById;

public sealed class GetTripByIdQueryHandler(ITripRepository _tripRepository): IRequestHandler<GetTripByIdQuery, Result<TripDetailsDto>>
{
  
    public async Task<Result<TripDetailsDto>> Handle(GetTripByIdQuery request,CancellationToken cancellationToken)
    {
        var trip = await _tripRepository.GetByIdAsync(request.Id,cancellationToken);

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
