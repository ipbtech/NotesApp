using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Services;

namespace NotesApp.Application.Services
{
    internal class TagService() : ITagService
    {

        public async Task<IEnumerable<Tag>> GetAllAsync(Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Tag?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Tag> CreateAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Tag> UpdateAsync(Guid id, string newName)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
