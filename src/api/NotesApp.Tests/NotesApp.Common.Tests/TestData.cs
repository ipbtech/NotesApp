using NotesApp.Domain.Entities;
using NotesApp.Domain.Enums;
using NotesApp.Domain.Utils;

namespace NotesApp.Common.Tests
{
    public static class TestData
    {
        public static string TestUserPassword => "te$$$T31";

        public static User TestUserAdmin => new()
        {
            Id = new Guid("844675E1-FF43-434B-88DD-55B021079A02"),
            Email = "test@test.com",
            PasswordHash = StringHasher.ToHash(TestUserPassword),
            Role = UserRole.Admin,
            CreatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime(),
            UpdatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime()
        };


        public static User TestUser => new()
        {
            Id = new Guid("FA5824FF-BF94-4854-91E3-B0F585A94C33"),
            Email = "test123@test.com",
            PasswordHash = StringHasher.ToHash(TestUserPassword),
            Role = UserRole.User,
            CreatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime(),
            UpdatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime()
        };


        public static List<Tag> TestTags = new()
        {
            new Tag()
            {
                Id = new Guid("7CF795DD-8DB0-42C4-9CF8-C1CC8A890C60"),
                Name = "Personal",
                UserId = TestUser.Id,
                CreatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime(),
                UpdatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime()
            },
            new Tag()
            {
                Id = new Guid("8AB795DD-8DB0-42C4-9CF8-C1CC8A890C61"),
                Name = "Work",
                UserId = TestUserAdmin.Id,
                CreatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime(),
                UpdatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime()
            },
            new Tag()
            {
                Id = new Guid("9BC795DD-8DB0-42C4-9CF8-C1CC8A890C62"),
                Name = "Travel",
                UserId = TestUser.Id,
                CreatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime(),
                UpdatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime()
            },
            new Tag()
            {
                Id = new Guid("1CD795DD-8DB0-42C4-9CF8-C1CC8A890C63"),
                Name = "Health",
                UserId = TestUser.Id,
                CreatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime(),
                UpdatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime()
            },
            new Tag()
            {
                Id = new Guid("2DE795DD-8DB0-42C4-9CF8-C1CC8A890C64"),
                Name = "Finance",
                UserId = TestUserAdmin.Id,
                CreatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime(),
                UpdatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime()
            },
            new Tag()
            {
                Id = new Guid("3EF795DD-8DB0-42C4-9CF8-C1CC8A890C65"),
                Name = "Education",
                UserId = TestUser.Id,
                CreatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime(),
                UpdatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime()
            },
            new Tag()
            {
                Id = new Guid("4FA795DD-8DB0-42C4-9CF8-C1CC8A890C66"),
                Name = "Hobbies",
                UserId = TestUserAdmin.Id,
                CreatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime(),
                UpdatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime()
            },
            new Tag()
            {
                Id = new Guid("5FB795DD-8DB0-42C4-9CF8-C1CC8A890C67"),
                Name = "Shopping",
                UserId = TestUser.Id,
                CreatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime(),
                UpdatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime()
            },
            new Tag()
            {
                Id = new Guid("6FC795DD-8DB0-42C4-9CF8-C1CC8A890C68"),
                Name = "Family",
                UserId = TestUser.Id,
                CreatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime(),
                UpdatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime()
            },
            new Tag()
            {
                Id = new Guid("7FD795DD-8DB0-42C4-9CF8-C1CC8A890C69"),
                Name = "Friends",
                UserId = TestUserAdmin.Id,
                CreatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime(),
                UpdatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime()
            }
        };
    }
}
