namespace NotesApp.Application.Dtos
{
    public record TagDto(
        Guid Id,
        string Name,
        Guid UserId,
        DateTimeOffset CreatedAtUtc,
        Guid CreatedBy,
        DateTimeOffset UpdatedAtUtc,
        Guid UpdatedBy);
}
