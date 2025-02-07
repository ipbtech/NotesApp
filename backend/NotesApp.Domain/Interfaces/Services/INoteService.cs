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
        public Task<NoteCreateDto> CreateAsync(NoteCreateDto noteCreateDto);
        public Task<NoteGetDto> UpdateAsync(NoteUpdateDto noteDto);
        public Task DeleteAsync(Guid id);
    }
}
