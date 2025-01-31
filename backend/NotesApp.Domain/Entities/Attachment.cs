using NotesApp.Domain.Entities.Base;

namespace NotesApp.Domain.Entities
{
    public class Attachment : BaseFile
    {
        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
