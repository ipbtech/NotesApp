using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApp.Domain.Entities;

namespace NotesApp.DAL.Configs
{
    public class NoteDbConfig : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(e => e.Name).HasMaxLength(255).IsRequired();

            builder.HasMany(e => e.Attachments).WithOne(e => e.Note)
                .HasForeignKey(e => e.NoteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Tag).WithMany(e => e.Notes)
                .HasForeignKey(e => e.TagId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
