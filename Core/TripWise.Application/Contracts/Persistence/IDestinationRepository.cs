

using TripWise.Domain.Entities;

namespace TripWise.Application.Contracts.Persistence;

public interface IDestinationRepository
{
    Task AddAsync(Destination destination,CancellationToken cancellationToken);
    Task<Destination?> GetByIdAsync(Guid id, CancellationToken cancellationToken,bool tracking = false);
    Task<IReadOnlyList<Destination>> GetAllAsync(int pageNumber,int pageSize,CancellationToken cancellationToken);
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken);
    Task UpdateAsync(Destination destination,CancellationToken cancellationToken);
    Task DeleteAsync(Destination destination,CancellationToken cancellationToken);
}