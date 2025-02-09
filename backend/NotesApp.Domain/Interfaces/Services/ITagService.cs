using NotesApp.Domain.Entities;

namespace NotesApp.Domain.Interfaces.Services
{
    public interface ITagService
    {
        public Task<IEnumerable<Tag>> GetAllAsync(Guid? userId = null);
        public Task<Tag?> GetByIdAsync(Guid id);
        public Task<Tag> CreateAsync(string name);
        public Task<Tag> UpdateAsync(Guid id, string newName);
        public Task DeleteAsync(Guid id);
    }
}
