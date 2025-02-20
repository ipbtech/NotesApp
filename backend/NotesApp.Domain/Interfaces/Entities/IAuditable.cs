namespace NotesApp.Domain.Interfaces.Entities
{
    public interface IAuditable
    {
        public DateTimeOffset CreatedAtUtc { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedAtUtc { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
