using NotesApp.Domain.Dtos;
using NotesApp.Domain.Enums;

namespace NotesApp.Domain.Interfaces.Services
{
    public interface INoteService
    {
        public Task<IEnumerable<NoteGetDto>> GetAllAsync(Guid? userId = null);
        public Task<IEnumerable<NoteGetDto>> GetAllAsync(
            IEnumerable<Guid> tagIds, NoteSortType sortType, int page = 1, Guid? userId = null);
        public Task<NoteGetDto> GetByIdAsync(Guid id);
        public Task<NoteCreateDto> CreateAsync(NoteCreateDto noteDto);
        public Task<NoteGetDto> UpdateAsync(Guid id, NoteUpdateDto noteDto);
        public Task DeleteAsync(Guid id);

        public Task ShareWithAnotherUserAsync(Guid noteId, Guid anotherUserId);
        // TODO public Task Unshare

        public Task NotifyAsync(Guid noteId, bool onlyForMe);
    }
}
