using NotesApp.Domain.Entities.Base;

namespace NotesApp.Domain.Entities
{
    public class Note : BaseEntity
    {
#nullable disable
        public string Name { get; set; }
#nullable enable
        public string? Description { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid? TagId { get; set; }
        public Tag? Tag { get; set; }

        public ICollection<User> AddedUsers { get; set; } = [];
        public ICollection<Attachment> Attachments { get; set; } = [];
    }
}
