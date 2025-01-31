using NotesApp.Domain.Dtos;

namespace NotesApp.Domain.Interfaces.Services
{
    public interface INoteService
    {
        public Task<IEnumerable<NoteDto>> GetNotesAsync(int? userId = null);
    }
}
