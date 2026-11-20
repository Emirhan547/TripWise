using MediatR;
using TripWise.Application.Abstractions.Identity;
using TripWise.Application.Common.Results;
using TripWise.Application.Contracts.Persistence;
using TripWise.Domain.Entities;

namespace TripWise.Application.Features.Trips.Commands.CreateTrip;

public sealed class CreateTripCommandHandler(
    ITripRepository tripRepository,
    ICurrentUserService currentUserService)
    : IRequestHandler<CreateTripCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        CreateTripCommand request,
        CancellationToken cancellationToken)
    {
        var trip = new Trip(currentUserService.UserId)
        {
            Name = request.Name,
            Description = request.Description!,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        await tripRepository.AddAsync(trip, cancellationToken);

        return Result<Guid>.Success(trip.Id);
    }
}