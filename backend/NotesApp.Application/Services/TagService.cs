using NotesApp.Domain.Dtos;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Repositories;
using NotesApp.Domain.Interfaces.Services;

namespace NotesApp.Application.Services
{
    internal class TagService(
        IRepository<Tag> tagRepository) : ITagService
    {
        public async Task<IEnumerable<TagGetDto>> GetAllAsync(Guid? userId = null)
        {
            if (userId.HasValue)
            {

            }
            return await tagRepository.ListAsync();
        }

        public Task<TagGetDto> CreateAsync(TagCreateOrUpdateDto tagDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }



        public Task<TagGetDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TagGetDto> UpdateAsync(Guid id, TagCreateOrUpdateDto tagDto)
        {
            throw new NotImplementedException();
        }
    }
}
