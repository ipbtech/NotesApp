using Microsoft.EntityFrameworkCore;
using NotesApp.DAL;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Mapping;
using NotesApp.Domain.Interfaces.Services;
using NotesApp.Dto;

namespace NotesApp.Application.Services
{
    internal class TagService(
        NotesAppDbContext dbContext,
        IMapper<Tag, TagResponseDto> mapper) : ITagService
    {
        
        public async Task<IEnumerable<TagResponseDto>> GetAllAsync(Guid? currentUserId = null)
        {
            List<Tag> tags;
            if (currentUserId.HasValue)
            {
                tags = await dbContext.Tags.Where(t => t.UserId == currentUserId).ToListAsync();
            }
            else
            {
                tags = await dbContext.Tags.ToListAsync();
            }

            return mapper.MapToDto(tags);
        }


        public async Task<TagResponseDto?> GetByIdAsync(Guid id, Guid currentUserId)
        {
            var tag = await dbContext.Tags.FindAsync(id) ??
                      throw new NullReferenceException("Tag does not exist");

            if (tag.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException("Unauthorized");
            }

            return mapper.MapToDto(tag);
        }


        public async Task<TagResponseDto> CreateAsync(string name, Guid currentUserId)
        {
            var tag = new Tag
            {
                Name = name,
                UserId = currentUserId
            };

            await dbContext.Tags.AddAsync(tag);
            await dbContext.SaveChangesAsync();

            return mapper.MapToDto(tag);
        }


        public async Task<TagResponseDto> UpdateAsync(Guid id, string newName, Guid currentUserId)
        {
            var tag = await dbContext.Tags.FindAsync(id) ??
                      throw new NullReferenceException("Tag does not exist");

            if (tag.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException("Unauthorized");
            }

            tag.Name = newName;
            dbContext.Tags.Update(tag);
            await dbContext.SaveChangesAsync();
            return mapper.MapToDto(tag);
        }


        public async Task DeleteAsync(Guid id, Guid currentUserId)
        {
            var tag = await dbContext.Tags.FindAsync(id) ??
                      throw new NullReferenceException("Tag does not exist");

            if (tag.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException("Unauthorized");
            }

            dbContext.Tags.Remove(tag);
            await dbContext.SaveChangesAsync();
        }
    }
}