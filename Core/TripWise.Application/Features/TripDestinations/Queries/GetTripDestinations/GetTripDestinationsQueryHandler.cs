using MediatR;
using TripWise.Application.Abstractions.Identity;
using TripWise.Application.Common.Results;
using TripWise.Application.Contracts.Persistence;

namespace TripWise.Application.Features.TripDestinations.Queries.GetTripDestinations;

public sealed class GetTripDestinationsQueryHandler(
    ITripRepository tripRepository,
    ITripDestinationRepository tripDestinationRepository,
    ICurrentUserService currentUserService)
    : IRequestHandler<
        GetTripDestinationsQuery,
        Result<List<TripDestinationListItemDto>>>
{
    public async Task<Result<List<TripDestinationListItemDto>>> Handle(
        GetTripDestinationsQuery request,
        CancellationToken cancellationToken)
    {
        var trip = await tripRepository.GetByIdAsync(
            request.TripId,
            currentUserService.UserId,
            cancellationToken);

        if (trip is null)
        {
            return Result<List<TripDestinationListItemDto>>.NotFound(
                "Seyahat bulunamadı.");
        }

        var tripDestinations =
            await tripDestinationRepository.GetByTripIdAsync(
                request.TripId,
                cancellationToken);

        var items = tripDestinations
            .Select(x => new TripDestinationListItemDto(
                x.Id,
                x.DestinationId,
                x.Destination.CityName,
                x.Destination.CountryName,
                x.Destination.CountryCode,
                x.VisitOrder,
                x.ArrivalDate,
                x.DepartureDate))
            .ToList();

        return Result<List<TripDestinationListItemDto>>.Success(items);
    }
}