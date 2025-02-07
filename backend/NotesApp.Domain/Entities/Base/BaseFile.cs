namespace NotesApp.Domain.Entities.Base
{
#nullable disable
    public abstract class BaseFile : BaseEntity
    {
        public string Name { get; set; }
        public string FileExtension { get; set; }
        public byte[] Content { get; set; }
    }
#nullable enable
}
