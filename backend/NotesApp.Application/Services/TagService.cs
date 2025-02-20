using NotesApp.Application.Specifications;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Repositories;
using NotesApp.Domain.Interfaces.Services;

namespace NotesApp.Application.Services
{
    internal class TagService(
        IRepository<Tag> tagRepository) : ITagService
    {

        public async Task<IEnumerable<Tag>> GetAllAsync(Guid? userId = null)
        {
            if (userId.HasValue)
            {
                return await tagRepository.ListAsync(new ByUserIdSpec<Tag>(userId.Value, true));
            }
            return await tagRepository.ListAsync(new AsNoTrackingSpec<Tag>());
        }

        public async Task<Tag?> GetByIdAsync(Guid id)
        {
            return await tagRepository.FirstOrDefaultAsync(new AsNoTrackingAndByIdSpec<Tag>(id));
        }

        public async Task<Tag> CreateAsync(string name)
        {
            var tag = new Tag 
            { 
                Name = name,
                //UserId = temporaryUser.User.Id //TODO
            };
            //TODO check if user already has passed tag 
            await tagRepository.AddAsync(tag);
            return tag;
        }

        public async Task<Tag> UpdateAsync(Guid id, string newName)
        {
            var tag = await tagRepository.GetByIdAsync(id) ??
                throw new NullReferenceException("Tag is not found");


            //TODO check if user already has passed tag 
            tag.Name = newName;
            await tagRepository.UpdateAsync(tag);
            return tag;
        }

        public async Task DeleteAsync(Guid id)
        {
            var tag = await tagRepository.GetByIdAsync(id) ??
                throw new NullReferenceException("Tag is not found");

            await tagRepository.DeleteAsync(tag);
        }
    }
}
