using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NotesApp.Domain.Entities.Base;

namespace NotesApp.DAL.Interceptors
{
    internal class DateInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var dbContext = eventData.Context;
            if (dbContext is null)
                return base.SavingChanges(eventData, result);

            var entries = dbContext.ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(e => e.CreatedAtUtc).CurrentValue = DateTime.UtcNow;
                    entry.Property(e => e.CreatedBy).CurrentValue = default;

                    entry.Property(e => e.UpdatedAtUtc).CurrentValue = DateTime.UtcNow;
                    entry.Property(e => e.UpdatedBy).CurrentValue = default;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property(e => e.UpdatedAtUtc).CurrentValue = DateTime.UtcNow;
                    entry.Property(e => e.UpdatedBy).CurrentValue = default;
                }
                //TODO Created and modified by
            }
            return base.SavingChanges(eventData, result);
        }
    }
}
