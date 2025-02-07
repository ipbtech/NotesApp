using NotesApp.Domain.Entities.Base;

namespace NotesApp.Domain.Entities
{
    public class User : BaseEntity
    {
#nullable disable
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
#nullable enable

        public Avatar? Avatar { get; set; }
        public ICollection<Note> PersonalNotes { get; set; } = [];
        public ICollection<Note> AllowedNotes { get; set; } = [];
        public ICollection<Tag> Tags { get; set; } = [];
    }
}
