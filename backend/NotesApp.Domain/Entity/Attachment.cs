using NotesApp.Domain.Entity.Base;

namespace NotesApp.Domain.Entity
{
    public class Attachment : BaseFile
    {
        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
