using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApp.Domain.Entities;

namespace NotesApp.DAL.Configs
{
    public class AttachmenDbConfig : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(e => e.Name).HasMaxLength(255).IsRequired();
            builder.Property(e => e.FileExtension).HasMaxLength(10).IsRequired();

            builder.HasOne(a => a.Note).WithMany(n => n.Attachments)
                .HasPrincipalKey(n => n.Id).HasForeignKey(a => a.NoteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
