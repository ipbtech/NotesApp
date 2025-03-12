using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Mapping;
using NotesApp.Dto;
using Riok.Mapperly.Abstractions;

namespace NotesApp.Application.Mapping
{
    [Mapper]
    public partial class UserMapper : IMapper<User, UserResponseDto>
    {
        public partial User MapFromDto(UserResponseDto dto);

        public partial UserResponseDto MapToDto(User entity);
    }
}