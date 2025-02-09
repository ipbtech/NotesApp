using NotesApp.Domain.Entities;

namespace NotesApp.Domain.Interfaces.Services
{
    public interface ITagService
    {
        public Task<IEnumerable<Tag>> GetAllAsync(Guid? userId = null);
        public Task<Tag> GetByIdAsync(Guid id);
        public Task<Tag> CreateAsync(Tag tagDto);
        public Task<Tag> UpdateAsync(Guid id, Tag tagDto);
        public Task DeleteAsync(Guid id);
    }
}
