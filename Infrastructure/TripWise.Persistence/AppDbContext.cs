using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TripWise.Domain.Entities;
using TripWise.Persistence.Identity;

namespace TripWise.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options): IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Trip> Trips => Set<Trip>();

    public DbSet<Destination> Destinations => Set<Destination>();

    public DbSet<TripDestination> TripDestinations => Set<TripDestination>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);

        modelBuilder.Entity<Trip>()
            .HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<Destination>()
            .HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<TripDestination>()
            .HasQueryFilter(x => !x.IsDeleted);
    }
}