using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Application.Services;
using NotesApp.Domain.Interfaces.Mapping;
using NotesApp.Domain.Interfaces.Services;

namespace NotesApp.Application
{
    public static class ServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<ITagService, TagService>();
            services.AddMappers();
        }

        private static void AddMappers(this IServiceCollection services)
        {
            var assemblyTypes = Assembly.GetExecutingAssembly().GetTypes().ToList();
            assemblyTypes.ForEach(implType =>
            {
                var srvType = implType.GetInterfaces()
                    .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IMapper<,>));

                if (srvType is not null)
                    services.AddSingleton(srvType, implType);
            });
        }
    }
}
