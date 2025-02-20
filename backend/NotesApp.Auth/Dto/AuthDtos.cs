namespace NotesApp.Auth.Dto
{
    public record LoginDto(
        string Email,
        string Password);

    public record SignUpDto(
        string Email,
        string Password,
        string ConfirmPassword);

    public record TokensDto(
        string AccessToken,
        string RefreshToken);

    public record RefreshTokenDto(
        Guid UserId,
        string RefreshToken);

    public record ChangePasswordDto(
        string OldPassword,
        string NewPassword);
}