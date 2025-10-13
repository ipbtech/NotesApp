using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Mapping;
using NotesApp.Dto;
using Riok.Mapperly.Abstractions;

namespace NotesApp.Application.Mapping
{
    [Mapper]
    public partial class NoteRequestMapper : IMapper<Note, NoteRequestDto>
    {
        public partial Note MapFromDto(NoteRequestDto dto);

        public partial NoteRequestDto MapToDto(Note entity);

        public partial void UpdateEntity(NoteRequestDto dto, Note entity);
    }


    [Mapper]
    public partial class NoteResponseMapper : IMapper<Note, NoteResponseDto>
    {
        public partial Note MapFromDto(NoteResponseDto dto);

        public partial NoteResponseDto MapToDto(Note entity);

        public partial void UpdateEntity(NoteResponseDto dto, Note entity);
    }
}
