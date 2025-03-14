using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Mapping;
using NotesApp.Dto;
using Riok.Mapperly.Abstractions;

namespace NotesApp.Application.Mapping
{
    [Mapper]
    public partial class AvatarMapper : IMapper<Avatar, UserAvatarDto>
    {
        [MapProperty(nameof(UserAvatarDto.FileName), nameof(Avatar.Name))]
        public partial Avatar MapFromDto(UserAvatarDto dto);

        [MapProperty(nameof(Avatar.Name), nameof(UserAvatarDto.FileName))]
        public partial UserAvatarDto MapToDto(Avatar entity);

        public partial void UpdateEntity(UserAvatarDto dto, Avatar entity);
    }
}
