using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripWise.Domain.Entities;

namespace TripWise.Persistence.Configurations;

public sealed class TripDestinationConfiguration: IEntityTypeConfiguration<TripDestination>
{
    public void Configure(EntityTypeBuilder<TripDestination> builder)
    {
        builder.HasOne(x => x.Trip).WithMany(x => x.TripDestinations).HasForeignKey(x => x.TripId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Destination).WithMany(x => x.TripDestinations).HasForeignKey(x => x.DestinationId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new{ x.TripId,x.DestinationId}).IsUnique().HasFilter("[IsDeleted] = 0");
    }
}