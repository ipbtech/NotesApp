namespace NotesApp.Auth.Dto
{
    public record LoginRequestDto(
        string Email,
        string Password);

    public record SignUpRequestDto(
        string Email,
        string Password,
        string ConfirmPassword) : LoginRequestDto(Email, Password);

    public record LoginResponseDto(
        string AccessToken,
        string RefreshToken);

    public record RefreshTokenDto(
        string RefreshToken);

    public record ChangePasswordDto(
        string OldPassword,
        string NewPassword);
}