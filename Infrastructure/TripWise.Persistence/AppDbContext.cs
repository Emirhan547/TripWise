using Microsoft.EntityFrameworkCore;
using TripWise.Domain.Entities;

namespace TripWise.Persistence;

public sealed class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Trip> Trips => Set<Trip>();

    public DbSet<Destination> Destinations => Set<Destination>();

    public DbSet<TripDestination> TripDestinations => Set<TripDestination>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.Entity<Trip>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Destination>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<TripDestination>().HasQueryFilter(x => !x.IsDeleted);
    }
}
