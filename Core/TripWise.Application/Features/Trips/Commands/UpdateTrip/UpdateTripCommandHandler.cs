using MediatR;
using TripWise.Application.Abstractions.Identity;
using TripWise.Application.Common.Results;
using TripWise.Application.Contracts.Persistence;

namespace TripWise.Application.Features.Trips.Commands.UpdateTrip;

public sealed class UpdateTripCommandHandler( ICurrentUserService currentUserService,ITripRepository _tripRepository) : IRequestHandler<UpdateTripCommand, Result<Guid>>
{

    public async Task<Result<Guid>> Handle( UpdateTripCommand request,CancellationToken cancellationToken)
    {
       

        var trip = await _tripRepository.GetByIdAsync(currentUserService.UserId, request.Id,cancellationToken,tracking: true);

        if (trip is null)
        {
            return Result<Guid>.NotFound("Seyahat bulunamadı.");
        }

        trip.Name = request.Name;
        trip.Description = request.Description;
        trip.StartDate = request.StartDate;
        trip.EndDate = request.EndDate;

        await _tripRepository.UpdateAsync(trip, cancellationToken);

        return Result<Guid>.Success(trip.Id);
    }
}
