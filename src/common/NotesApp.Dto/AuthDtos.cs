namespace NotesApp.Dto
{
    public record LoginRequestDto(
        string Email,
        string Password
    );

    public record SignUpRequestDto(
        string Email,
        string Password,
        string ConfirmPassword
    );

    public record LoginResponseDto(
        string AccessToken,
        string RefreshToken
    );

    public record RefreshTokenRequestDto(
        Guid UserId,
        string RefreshToken
    );

    public record RefreshTokenResponseDto(
        string AccessToken
    );

    public record ChangePasswordDto(
        string OldPassword,
        string NewPassword
    );
}