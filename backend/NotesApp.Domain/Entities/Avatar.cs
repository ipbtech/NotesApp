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
        public DateTimeOffset CreatedAtUtc { get; set; }
        public DateTimeOffset UpdatedAtUtc { get; set; }

        //IFile impl
#nullable disable
        public string Name { get; set; }
        public string FileExtension { get; set; }
        public byte[] Content { get; set; }
#nullable enable
    }
}
