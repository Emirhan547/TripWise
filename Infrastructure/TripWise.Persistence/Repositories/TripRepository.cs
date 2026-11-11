

using Microsoft.EntityFrameworkCore;
using TripWise.Application.Contracts.Persistence;
using TripWise.Domain.Entities;

namespace TripWise.Persistence.Repositories;

public sealed class TripRepository (AppDbContext _context) : ITripRepository
{
  
    public async Task AddAsync(Trip trip,CancellationToken cancellationToken)
    {
       await _context.Trips.AddAsync(trip);

        await _context.SaveChangesAsync(cancellationToken);
    }


    public async Task<IReadOnlyList<Trip>> GetAllAsync(int pageNumber,int pageSize,CancellationToken cancellationToken)
    {
        return await _context.Trips.AsNoTracking().OrderBy(x => x.CreatedAt).ThenBy(x => x.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }
    public async Task<Trip?> GetByIdAsync(Guid id,CancellationToken cancellationToken,bool tracking = false)
    {
        var query = _context.Trips.AsQueryable();

        if (!tracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
    }
    public async Task UpdateAsync(Trip trip,CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteAsync(Trip trip,CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken)
    {
        return await _context.Trips.CountAsync(cancellationToken);
    }
}
