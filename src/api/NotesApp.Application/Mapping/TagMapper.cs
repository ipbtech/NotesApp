using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Mapping;
using NotesApp.Dto;
using Riok.Mapperly.Abstractions;

namespace NotesApp.Application.Mapping
{
    [Mapper]
    public partial class TagMapper : IMapper<Tag, TagResponseDto>
    {
        public partial Tag MapFromDto(TagResponseDto dto);

        public partial TagResponseDto MapToDto(Tag entity);

        public partial void UpdateEntity(TagResponseDto dto, Tag entity);
    }
}
