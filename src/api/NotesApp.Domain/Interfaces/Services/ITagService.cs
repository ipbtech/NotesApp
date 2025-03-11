using NotesApp.Dto;

namespace NotesApp.Domain.Interfaces.Services
{
    public interface ITagService
    {
        public Task<IEnumerable<TagResponseDto>> GetAllAsync(Guid? currentUserId = null);
        public Task<TagResponseDto?> GetByIdAsync(Guid id, Guid currentUserId);
        public Task<TagResponseDto> CreateAsync(string name, Guid currentUserId);
        public Task<TagResponseDto> UpdateAsync(Guid id, string newName, Guid currentUserId);
        public Task DeleteAsync(Guid id, Guid currentUserId);
    }
}
