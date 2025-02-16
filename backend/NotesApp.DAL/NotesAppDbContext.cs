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
        private readonly SeedUser _seedUser;
        private readonly IWebHostEnvironment _environment;
        public NotesAppDbContext(
            DbContextOptions<NotesAppDbContext> options,
            IWebHostEnvironment environment,
            IOptions<SeedUser> seedUserOptions) : base(options)
        {
            _seedUser = seedUserOptions.Value;
            _environment = environment;

            if (Database.GetPendingMigrations().Any())
                Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //TODO indexes

            if (_environment.IsDevelopment())
            {
                modelBuilder.Entity<User>().HasData(new User
                {
                    Id = _seedUser.Id,
                    Email = _seedUser.Email,
                    UserName = _seedUser.UserName,
                    Password = _seedUser.Password,
                    Role = _seedUser.Role,
                    CreatedAtUtc = new DateTime(2025, 1, 1),
                    CreatedBy = Guid.Empty,
                    UpdatedAtUtc = new DateTime(2025, 1, 1),
                    UpdatedBy = Guid.Empty,
                });
            }
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
