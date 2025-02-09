namespace NotesApp.Application.Dtos
{
    public record TagDto(
        Guid Id,
        string Name,
        Guid UserId,
        DateTime CreatedAtUtc,
        Guid CreatedBy,
        DateTime UpdatedAtUtc,
        Guid UpdatedBy);
}
