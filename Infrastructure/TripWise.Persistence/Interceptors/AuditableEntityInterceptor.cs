using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;

using TripWise.Domain.Abstractions;

namespace TripWise.Persistence.Interceptors;

public sealed class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,InterceptionResult<int> result,CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context is not null)
        {
            var now = DateTimeOffset.Now;

            foreach (var entry in context.ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property(x => x.CreatedAt).CurrentValue = now;
                        entry.Property(x => x.IsDeleted).CurrentValue = false;
                        break;

                    case EntityState.Modified:
                        entry.Property(x => x.UpdatedAt).CurrentValue = now;
                        entry.Property(x => x.UpdatedAt).IsModified = true;

                        entry.Property(x => x.CreatedAt).IsModified = false;
                        entry.Property(x => x.IsDeleted).IsModified = false;
                        entry.Property(x => x.DeletedAt).IsModified = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;

                        entry.Property(x => x.IsDeleted).CurrentValue = true;
                        entry.Property(x => x.IsDeleted).IsModified = true;

                        entry.Property(x => x.DeletedAt).CurrentValue = now;
                        entry.Property(x => x.DeletedAt).IsModified = true;

                        entry.Property(x => x.UpdatedAt).CurrentValue = now;
                        entry.Property(x => x.UpdatedAt).IsModified = true;
                        break;
                }
            }
        }

        return base.SavingChangesAsync( eventData,result,cancellationToken);
    }
}