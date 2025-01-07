namespace NotesApp.Domain.Entity.Base
{
    public abstract class BaseFile : BaseEntity
    {
        public string Name { get; set; }
        public string FileExtension { get; set; }
        public byte[] Content { get; set; }
    }
}
