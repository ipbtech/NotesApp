using NotesApp.Domain.Entity.Base;

namespace NotesApp.Domain.Entity
{
    public class Avatar : BaseFile
    {
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
