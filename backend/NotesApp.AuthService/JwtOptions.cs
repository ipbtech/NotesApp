namespace NotesApp.AuthService
{
    internal class JwtOptions
    {
        public const string OptionName = "JwtOptions";

        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int AccessTokenExpirationMinutes { get; set; } 
        public int RefreshTokenExpirationDays { get; set; }
    }
}
