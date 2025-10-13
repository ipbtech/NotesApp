namespace NotesApp.Domain.Utils
{
    public static class StringHasher
    {
        public static string ToHash(string data)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            return BCrypt.Net.BCrypt.HashPassword(data, salt);
        }

        public static bool VerifyHash(string passed, string hashed)
        {
            return BCrypt.Net.BCrypt.Verify(passed, hashed);
        }
    }
}
