using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Domain.Entities
{
    public class RefreshToken : IEntityId, IUserSpecific
    {
#nullable disable
        public string TokenHash { get; set; }
#nullable enable 
        public Guid DeviceSessionId { get; set; }
        public DateTimeOffset ExpiredDateTimeUtc { get; set; }

        //IEntityId impl
        public Guid Id { get; set; }

        //IUserSpecific impl
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
