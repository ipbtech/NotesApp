using NotesApp.Domain.Entities.Base;

namespace NotesApp.Domain.Entities
{
    public class Tag : BaseEntity
    {
#nullable disable
        public string Name { get; set; }
#nullable enable

        public Guid UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Note> Notes { get; set; } = [];
    }
}
