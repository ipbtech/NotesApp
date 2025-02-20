using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Domain.Entities
{
    public class Tag : IEntityId, IAuditable, IUserSpecific
    {
#nullable disable
        public string Name { get; set; }
#nullable enable

        //IUserSpecific impl
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Note> Notes { get; set; } = [];

        //IModelId impl
        public Guid Id { get; set; }

        //IAuditable impl
        public DateTimeOffset CreatedAtUtc { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedAtUtc { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
