using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NotesApp.DAL;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Mapping;
using NotesApp.Domain.Interfaces.Services;
using NotesApp.Domain.Utils;
using NotesApp.Dto;

namespace NotesApp.Application.Services
{
    internal class AvatarService(
        NotesAppDbContext dbContext,
        IMapper<Avatar, UserAvatarDto> mapper) : IAvatarService
    {
        public Task<UserAvatarDto?> GetAsync(Guid userId)
        {
            return dbContext.Avatars
                .Where(a => a.UserId == userId)
                .Select(a => new UserAvatarDto(a.Name, a.FileExtension, a.Content))
                .FirstOrDefaultAsync();
        }

        public async Task<UserAvatarDto> UploadAsync(Guid userId, IFormFile formFile)
        {
            var current = await dbContext.Avatars
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (current is not null)
            {
                dbContext.Avatars.Remove(current);
            }

            var content = await formFile.ToBytesAsync();
            var avatar = new Avatar()
            {
                UserId = userId,
                Content = content,
                Name = formFile.FileName,
                FileExtension = Path.GetExtension(formFile.FileName).Replace(".", "")
            };

            await dbContext.Avatars.AddAsync(avatar);
            await dbContext.SaveChangesAsync();
            return mapper.MapToDto(avatar);
        }

        public async Task DeleteAsync(Guid userId)
        {
            var avatar = await dbContext.Avatars.FirstOrDefaultAsync(a => a.UserId == userId);
            if (avatar is not null)
            {
                dbContext.Avatars.Remove(avatar);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}