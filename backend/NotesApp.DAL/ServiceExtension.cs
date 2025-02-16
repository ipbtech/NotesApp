using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.DAL.Repositories;
using NotesApp.Domain.Interfaces.Repositories;

namespace NotesApp.DAL
{
    public static class ServiceExtension
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("DockerDefault");
            services.AddDbContext<NotesAppDbContext>(opt => opt.UseNpgsql(connStr));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.Configure<SeedUser>(configuration.GetSection(SeedUser.OptionName));
        }
    }
}
