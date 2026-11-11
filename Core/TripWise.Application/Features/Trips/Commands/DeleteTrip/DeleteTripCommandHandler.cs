using MediatR;
using TripWise.Application.Common.Results;
using TripWise.Application.Contracts.Persistence;

namespace TripWise.Application.Features.Trips.Commands.DeleteTrip;

public sealed class DeleteTripCommandHandler(ITripRepository _tripRepository) : IRequestHandler<DeleteTripCommand, Result<Guid>>
{
   
    public async Task<Result<Guid>> Handle(DeleteTripCommand request,CancellationToken cancellationToken)
    {
        var trip = await _tripRepository.GetByIdAsync( request.Id, cancellationToken,tracking: true);

        if (trip is null)
        {
            return Result<Guid>.NotFound("Seyahat bulunamadı.");
        }

        trip.Delete();
        await _tripRepository.DeleteAsync(trip, cancellationToken);

        return Result<Guid>.Success(trip.Id);
    }
}