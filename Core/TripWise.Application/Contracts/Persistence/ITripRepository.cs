

using TripWise.Domain.Entities;

namespace TripWise.Application.Contracts.Persistence;

public interface ITripRepository
{
    Task AddAsync(Trip trip,CancellationToken cancellationToken);
    Task<Trip?> GetByIdAsync(Guid id,CancellationToken cancellationToken,bool tracking = false);
    Task<IReadOnlyList<Trip>> GetAllAsync(int pageNumber, int pageSize,CancellationToken cancellationToken);
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken);
    Task UpdateAsync(Trip trip, CancellationToken cancellationToken);
    Task DeleteAsync(Trip trip, CancellationToken cancellationToken);
}
