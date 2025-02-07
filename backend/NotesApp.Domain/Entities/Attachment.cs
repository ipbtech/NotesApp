using NotesApp.Domain.Entities.Base;

namespace NotesApp.Domain.Entities
{
    public class Attachment : BaseFile
    {
        public Guid NoteId { get; set; }
        public Note? Note { get; set; }
    }
}
