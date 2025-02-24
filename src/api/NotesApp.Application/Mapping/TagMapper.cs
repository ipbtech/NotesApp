using NotesApp.Application.Dtos;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Mapping;
using Riok.Mapperly.Abstractions;

namespace NotesApp.Application.Mapping
{
    [Mapper]
    public partial class TagMapper : IMapper<Tag, TagDto>
    {
        public partial Tag MapFromDto(TagDto dto);

        public partial TagDto MapToDto(Tag entity);
    }
}
