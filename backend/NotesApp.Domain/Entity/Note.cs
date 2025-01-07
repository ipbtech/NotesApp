using NotesApp.Domain.Entity.Base;

namespace NotesApp.Domain.Entity
{
    public class Note : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<User> AddedUsers { get; set; } = [];
        public ICollection<Attachment> Attachments { get; set; } = [];
    }
}
