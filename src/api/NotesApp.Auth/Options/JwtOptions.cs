﻿namespace NotesApp.Auth.Options
{
    public class JwtOptions
    {
        public const string OptionName = "JwtOptions";

        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public double AccessTokenExpirationMinutes { get; set; }
        public double RefreshTokenExpirationDays { get; set; }
    }
}
