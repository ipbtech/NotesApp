using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.DAL.Interceptors;
using NotesApp.DAL.Repositories;
using NotesApp.Domain.Entities;

namespace NotesApp.DAL
{
    public static class ServiceExtension
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("DockerDefault");
            services.AddDbContext<NotesAppDbContext>(opt => opt.UseNpgsql(connStr));
            services.AddSingleton<DateInterceptor>();
            services.AddRepositories();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            //services.AddScoped<IRepository<User>, BaseRepository<User>>();
            //services.AddScoped<IRepository<Avatar>, BaseRepository<Avatar>>();
            //services.AddScoped<IRepository<Note>, BaseRepository<Note>>();
            //services.AddScoped<IRepository<Attachment>, BaseRepository<Attachment>>();
        }
    }
}
