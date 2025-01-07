namespace NotesApp.Domain.Entity.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastModifiedAtUtc { get; set; }
        public int LastModifiedBy { get; set; }
    }
}
