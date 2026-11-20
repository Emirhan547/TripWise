using Microsoft.EntityFrameworkCore;
using TripWise.Application.Contracts.Persistence;
using TripWise.Domain.Entities;

namespace TripWise.Persistence.Repositories;

public sealed class TripDestinationRepository(AppDbContext context) : ITripDestinationRepository
{
    public async Task<bool> ExistsAsync(Guid tripId,Guid destinationId,CancellationToken cancellationToken)
    {
        return await context.TripDestinations.AnyAsync(x => x.TripId == tripId &&x.DestinationId == destinationId,cancellationToken);
    }

    public async Task<int> GetNextVisitOrderAsync(Guid tripId,CancellationToken cancellationToken)
    {
        var lastVisitOrder = await context.TripDestinations.Where(x => x.TripId == tripId).Select(x => (int?)x.VisitOrder).MaxAsync(cancellationToken);
        return (lastVisitOrder ?? 0) + 1;
    }

    public async Task AddAsync(TripDestination tripDestination,CancellationToken cancellationToken)
    {
        await context.TripDestinations.AddAsync(tripDestination,cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IReadOnlyList<TripDestination>> GetByTripIdAsync(
    Guid tripId,
    CancellationToken cancellationToken)
    {
        return await context.TripDestinations
            .AsNoTracking()
            .Include(x => x.Destination)
            .Where(x => x.TripId == tripId)
            .OrderBy(x => x.VisitOrder)
            .ThenBy(x => x.Id)
            .ToListAsync(cancellationToken);
    }
    public async Task<TripDestination?> GetByTripAndDestinationAsync(
    Guid tripId,
    Guid destinationId,
    CancellationToken cancellationToken,
    bool tracking = false)
    {
        IQueryable<TripDestination> query = context.TripDestinations;

        if (!tracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(
            x => x.TripId == tripId &&
                 x.DestinationId == destinationId,
            cancellationToken);
    }
    public async Task DeleteAsync(
    TripDestination tripDestination,
    CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}