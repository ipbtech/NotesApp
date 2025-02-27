namespace NotesApp.Dto
{
    public record UserResponseDto(
        Guid Id,
        string Email,
        string UserName,
        string Role);

    public record UserRequestDto(
        string UserName);
}