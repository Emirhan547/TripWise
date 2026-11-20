using MediatR;
using TripWise.Application.Abstractions.Identity;
using TripWise.Application.Common.Results;
using TripWise.Application.Contracts.Persistence;
using TripWise.Domain.Entities;

namespace TripWise.Application.Features.TripDestinations.Commands.AddDestinationToTrip;

public sealed class AddDestinationToTripCommandHandler(ITripRepository tripRepository,IDestinationRepository destinationRepository,ITripDestinationRepository tripDestinationRepository,ICurrentUserService currentUserService): IRequestHandler<AddDestinationToTripCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddDestinationToTripCommand request,CancellationToken cancellationToken)
    {
        var trip = await tripRepository.GetByIdAsync(request.TripId,currentUserService.UserId,cancellationToken);

        if (trip is null)
        {
            return Result<Guid>.NotFound("Seyahat bulunamadı.");
        }

        var destination = await destinationRepository.GetByIdAsync(request.DestinationId,cancellationToken);

        if (destination is null)
        {
            return Result<Guid>.NotFound("Destinasyon bulunamadı.");
        }

        if (request.ArrivalDate < trip.StartDate ||
            request.DepartureDate > trip.EndDate)
        {
            return Result<Guid>.Fail("TripDestination.InvalidDates",
                new[]
                {
                    "Destinasyon ziyaret tarihleri seyahat tarihleri içerisinde olmalıdır."
                });
        }

        var alreadyExists = await tripDestinationRepository.ExistsAsync(request.TripId,request.DestinationId,cancellationToken);

        if (alreadyExists)
        {
            return Result<Guid>.Fail("TripDestination.AlreadyExists",
                new[]
                {
                    "Bu destinasyon seyahate daha önce eklenmiş."
                });
        }

        var visitOrder = await tripDestinationRepository.GetNextVisitOrderAsync(request.TripId,cancellationToken);

        var tripDestination = new TripDestination
        {
            TripId = trip.Id,
            DestinationId = destination.Id,
            VisitOrder = visitOrder,
            ArrivalDate = request.ArrivalDate,
            DepartureDate = request.DepartureDate
        };

        await tripDestinationRepository.AddAsync(tripDestination,cancellationToken);

        return Result<Guid>.Success(tripDestination.Id);
    }
}