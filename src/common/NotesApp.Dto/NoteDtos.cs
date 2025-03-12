namespace NotesApp.Dto
{
    public record NoteRequestDto(
        string Name,
        string Description,
        Guid? TagId
    );

    public record NoteResponseDto(
        Guid Id,
        Guid UserId,
        Guid? TagId,
        string Name,
        string Description,
        DateTimeOffset CreatedAtUtc,
        DateTimeOffset UpdatedAtUtc
    );

    public record NoteTagUpdatingDto(
        Guid NoteId,
        Guid? TagId
    );

    public record NoteNotificationDto(
        Guid NoteId,
        bool ByEmail,
        bool ByTelegram
    );

    public record NotePaginationDto(
        string SearchRequest,
        Guid[] TagIds,
        NoteSortType Sorting,
        int PageNumber
    );

    public enum NoteSortType
    {
        Descending = 1,
        Ascending = 2,
    }
}
