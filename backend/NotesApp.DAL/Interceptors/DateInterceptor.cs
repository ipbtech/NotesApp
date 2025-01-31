using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NotesApp.Domain.Entities.Base;

namespace NotesApp.DAL.Interceptors
{
    public class DateInterceptor : SaveChangesInterceptor
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
                    entry.Property(e => e.CreatedAtUtc).CurrentValue = DateTime.UtcNow;

                if (entry.State == EntityState.Modified)
                    entry.Property(e => e.LastModifiedAtUtc).CurrentValue = DateTime.UtcNow;

                //TODO Created and modified by
            }
            return base.SavingChanges(eventData, result);
        }
    }
}
