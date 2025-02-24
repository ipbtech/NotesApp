namespace NotesApp.Domain.Interfaces.Entities
{
    public interface IFile
    {
        public string Name { get; set; }
        public string FileExtension { get; set; }
        public byte[] Content { get; set; }
    }
}
