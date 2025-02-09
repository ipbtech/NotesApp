using Microsoft.Extensions.DependencyInjection;
using NotesApp.Application.Services;
using NotesApp.Domain.Interfaces.Services;

namespace NotesApp.Application
{
    public static class ServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<TemporaryUser>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
        }
    }
}
