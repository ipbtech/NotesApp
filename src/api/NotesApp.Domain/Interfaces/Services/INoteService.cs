using NotesApp.Domain.Entities;
using NotesApp.Domain.Enums;

namespace NotesApp.Domain.Interfaces.Services
{
    public interface INoteService
    {
        public Task<IEnumerable<Note>> GetAllAsync(Guid? userId = null);
        public Task<IEnumerable<Note>> GetAllAsync(
            IEnumerable<Guid> tagIds, NoteSortType sortType, int page = 1, Guid? userId = null);
        public Task<Note> GetByIdAsync(Guid id);
        public Task<Note> CreateAsync(Note noteDto);
        public Task<Note> UpdateAsync(Guid id, Note noteDto);
        public Task DeleteAsync(Guid id);

        public Task ShareWithAnotherUserAsync(Guid noteId, Guid anotherUserId);
        // TODO public Task Unshare
        public Task NotifyAsync(Guid noteId, bool onlyForMe);
    }
}
