using NotesApp.Domain.Entities;
using NotesApp.Domain.Enums;
using NotesApp.Domain.Utils;

namespace NotesApp.Common.Tests
{
    public static class TestData
    {
        public static string TestUserPassword => "te$$$T31";

        public static User TestUser => new User
        {
            Id = new Guid("844675E1-FF43-434B-88DD-55B021079A02"),
            Email = "test@test.com",
            PasswordHash = StringHasher.ToHash(TestUserPassword),
            Role = UserRole.Admin,
            CreatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime(),
            UpdatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime()
        };
    }
}
