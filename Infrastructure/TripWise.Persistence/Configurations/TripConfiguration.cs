using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TripWise.Domain.Entities;

namespace TripWise.Persistence.Configurations;

public sealed class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.ToTable("Trips");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(150);

        builder.Property(x => x.Description).HasMaxLength(1000);

        builder.Property(x => x.StartDate).HasColumnType("date");

        builder.Property(x => x.EndDate).HasColumnType("date");

        builder.Property(x => x.Status).HasConversion<int>();

        builder.HasIndex(x => x.Status);

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.UpdatedAt);


        builder.Property(x => x.IsDeleted).IsRequired();

        builder.Property(x => x.DeletedAt);
    }
}