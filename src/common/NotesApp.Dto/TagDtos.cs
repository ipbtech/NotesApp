namespace NotesApp.Dto
{
    public record TagResponseDto(
        Guid Id,
        Guid UserId,
        string Name);
}