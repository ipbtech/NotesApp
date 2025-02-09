using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Domain.Entities
{
    public class Avatar : IEntityId, IAuditable, IFile
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }

        //IModelId impl
        public Guid Id { get; set; }

        //IAuditable impl
        public DateTime CreatedAtUtc { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAtUtc { get; set; }
        public Guid UpdatedBy { get; set; }

        //IFile impl
#nullable disable
        public string Name { get; set; }
        public string FileExtension { get; set; }
        public byte[] Content { get; set; }
#nullable enable
    }
}
