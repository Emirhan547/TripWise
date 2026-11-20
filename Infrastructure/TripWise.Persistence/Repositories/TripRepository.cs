

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


    public async Task<IReadOnlyList<Trip>> GetAllAsync(Guid userId,int pageNumber,int pageSize,CancellationToken cancellationToken)
    {
        return await _context.Trips.AsNoTracking().Where(x => x.UserId == userId) .OrderBy(x => x.CreatedAt).ThenBy(x => x.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }
    public async Task<Trip?> GetByIdAsync(Guid id,Guid userId,CancellationToken cancellationToken,bool tracking = false)
    {
        IQueryable<Trip> query = _context.Trips;

        if (!tracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId,cancellationToken);
    }
    public async Task UpdateAsync(Trip trip,CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteAsync(Trip trip,CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<int> GetTotalCountAsync(Guid userId,CancellationToken cancellationToken)
    {
        return await _context.Trips.CountAsync(x => x.UserId == userId, cancellationToken);
    }
}
