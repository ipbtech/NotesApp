using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.DAL.Repositories;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Repositories;

namespace NotesApp.DAL
{
    public static class ServiceExtension
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            var seedUser = configuration.GetSection(SeedUser.OptionName).Get<User>();
            seedUser.CreatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime();
            seedUser.CreatedBy = Guid.Empty;
            seedUser.UpdatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime();
            seedUser.UpdatedBy = Guid.Empty;

            var connStr = configuration.GetConnectionString("DockerDefault");
            services.AddDbContext<NotesAppDbContext>(opt =>
            {
                opt.UseNpgsql(connStr);
                opt.UseSeeding((dbContext, _) =>
                {
                    var dbSet = dbContext.Set<User>();
                    if (!dbSet.Any(u => u.Id == seedUser.Id))
                    {
                        dbContext.Set<User>().Add(seedUser);
                        dbContext.SaveChanges();
                    }
                });
            });
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}