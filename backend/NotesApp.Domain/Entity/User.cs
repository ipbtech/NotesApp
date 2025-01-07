using NotesApp.Domain.Entity.Base;

namespace NotesApp.Domain.Entity
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int AvatarId { get; set; }
        public Avatar Avatar { get; set; }

        public ICollection<Note> PersonalNotes { get; set; } = [];
        public ICollection<Note> AllowedNotes { get; set; } = [];
    }
}
