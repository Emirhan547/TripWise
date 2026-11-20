using Microsoft.EntityFrameworkCore;
using TripWise.Application.Contracts.Persistence;
using TripWise.Domain.Entities;

namespace TripWise.Persistence.Repositories;

public sealed class DestinationRepository( AppDbContext context) : IDestinationRepository
{
    public async Task AddAsync(Destination destination,CancellationToken cancellationToken)
    {
        await context.Destinations.AddAsync(destination,cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Destination?> GetByIdAsync(Guid id, CancellationToken cancellationToken, bool tracking = false)
    {
        IQueryable<Destination> query = context.Destinations;

        if (!tracking)query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync( x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Destination>> GetAllAsync(int pageNumber,int pageSize,CancellationToken cancellationToken)
    {
        return await context.Destinations.AsNoTracking().OrderBy(x => x.CreatedAt).ThenBy(x => x.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken)
    {
        return await context.Destinations.CountAsync(cancellationToken);
    }

    public async Task UpdateAsync( Destination destination,CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Destination destination,CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}