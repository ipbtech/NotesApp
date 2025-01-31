using NotesApp.Domain.Entities.Base;

namespace NotesApp.Domain.Entities
{
    public class Avatar : BaseFile
    {
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
