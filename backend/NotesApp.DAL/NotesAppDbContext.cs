using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.DAL
{
    public class NotesAppDbContext : DbContext
    {
        public NotesAppDbContext(
            DbContextOptions<NotesAppDbContext> options) : base(options)
        {
            if (Database.GetPendingMigrations().Any())
                Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //TODO indexes
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<IAuditable>().ToList();
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
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
