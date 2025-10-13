using Microsoft.AspNetCore.Http;
using NotesApp.Dto;

namespace NotesApp.Domain.Interfaces.Services
{
    public interface IAvatarService
    {
        public Task<UserAvatarDto?> GetAsync(Guid userId);
        public Task<UserAvatarDto> UploadAsync(Guid userId, IFormFile formFile);
        public Task DeleteAsync(Guid userId);
    }
}
