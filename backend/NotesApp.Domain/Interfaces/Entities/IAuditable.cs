namespace NotesApp.Domain.Interfaces.Entities
{
    public interface IAuditable
    {
        public DateTime CreatedAtUtc { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAtUtc { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
