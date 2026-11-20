using MediatR;
using TripWise.Application.Abstractions.Identity;
using TripWise.Application.Common.Results;
using TripWise.Application.Contracts.Persistence;

namespace TripWise.Application.Features.TripDestinations.Commands.RemoveDestinationFromTrip;

public sealed class RemoveDestinationFromTripCommandHandler(
    ITripRepository tripRepository,
    ITripDestinationRepository tripDestinationRepository,
    ICurrentUserService currentUserService)
    : IRequestHandler<RemoveDestinationFromTripCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        RemoveDestinationFromTripCommand request,
        CancellationToken cancellationToken)
    {
        var trip = await tripRepository.GetByIdAsync(
            request.TripId,
            currentUserService.UserId,
            cancellationToken);

        if (trip is null)
        {
            return Result<Guid>.NotFound(
                "Seyahat bulunamadı.");
        }

        var tripDestination =
            await tripDestinationRepository.GetByTripAndDestinationAsync(
                request.TripId,
                request.DestinationId,
                cancellationToken,
                tracking: true);

        if (tripDestination is null)
        {
            return Result<Guid>.NotFound(
                "Bu destinasyon seyahatte bulunamadı.");
        }

        tripDestination.Delete();

        await tripDestinationRepository.DeleteAsync(
            tripDestination,
            cancellationToken);

        return Result<Guid>.Success(tripDestination.Id);
    }
}