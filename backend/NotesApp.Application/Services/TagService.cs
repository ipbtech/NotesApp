using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Repositories;
using NotesApp.Domain.Interfaces.Services;

namespace NotesApp.Application.Services
{
    internal class TagService(
        IRepository<Tag> tagRepository) : ITagService
    {
        public Task<Tag> CreateAsync(Tag tagDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tag>> GetAllAsync(Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> UpdateAsync(Guid id, Tag tagDto)
        {
            throw new NotImplementedException();
        }
    }
}
