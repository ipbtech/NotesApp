using NotesApp.Domain.Dtos;

namespace NotesApp.Domain.Interfaces.Services
{
    public interface INoteService
    {
        public Task GetNotesAsync(int? userId = null);
    }
}
