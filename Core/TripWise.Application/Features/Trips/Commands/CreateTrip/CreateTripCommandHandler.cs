
using MediatR;
using TripWise.Application.Common.Results;
using TripWise.Application.Contracts.Persistence;
using TripWise.Domain.Entities;

namespace TripWise.Application.Features.Trips.Commands.CreateTrip;

public sealed class CreateTripCommandHandler(ITripRepository tripRepository) : IRequestHandler<CreateTripCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateTripCommand request, CancellationToken cancellationToken)
    {
        var trip = new Trip()
        {
            Name = request.Name,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };
        await tripRepository.AddAsync(trip, cancellationToken);
        return Result<Guid>.Success(trip.Id);
    }
}
