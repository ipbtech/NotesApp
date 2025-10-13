using NotesApp.Dto;

namespace NotesApp.Domain.Interfaces.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<UserResponseDto>> GetAllAsync();
        public Task<UserResponseDto?> GetAsync(Guid id);
        public Task<UserResponseDto> UpdateAsync(Guid id, UserRequestDto userDto);
        public Task DeleteAsync(Guid id);
    }
}