using TripWise.Domain.Entities;

namespace TripWise.Application.Contracts.Persistence;

public interface ITripDestinationRepository
{
    Task<bool> ExistsAsync(Guid tripId,Guid destinationId, CancellationToken cancellationToken);
    Task<int> GetNextVisitOrderAsync(Guid tripId,CancellationToken cancellationToken);
    Task AddAsync(TripDestination tripDestination,CancellationToken cancellationToken);
    Task<IReadOnlyList<TripDestination>> GetByTripIdAsync(Guid tripId,CancellationToken cancellationToken);
    Task<TripDestination?> GetByTripAndDestinationAsync(Guid tripId,Guid destinationId,CancellationToken cancellationToken,bool tracking = false);
    Task DeleteAsync(TripDestination tripDestination,CancellationToken cancellationToken);
}