using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Domain.Entities
{
    public class Note : IEntityId, IAuditable, IUserSpecific
    {
#nullable disable
        public string Name { get; set; }
        public string Description { get; set; }
#nullable enable

        //IUserSpecific impl
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid? TagId { get; set; }
        public Tag? Tag { get; set; }

        public ICollection<User> AddedUsers { get; set; } = [];

        //IModelId impl
        public Guid Id { get; set; }

        //IAuditable impl
        public DateTimeOffset CreatedAtUtc { get; set; }
        public DateTimeOffset UpdatedAtUtc { get; set; }
    }
}
