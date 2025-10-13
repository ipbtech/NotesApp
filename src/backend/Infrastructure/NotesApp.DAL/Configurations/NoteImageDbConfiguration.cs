using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApp.Domain.Entities;

namespace NotesApp.DAL.Configurations;

internal class NoteImageDbConfiguration : IEntityTypeConfiguration<NoteImage>
{
    public void Configure(EntityTypeBuilder<NoteImage> builder)
    {
        builder.Property(a => a.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.OriginalFileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.BucketName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.ObjectName)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasIndex(a => new { a.NoteId, a.Order })
            .IsUnique();

        builder.HasOne(a => a.Note)
            .WithMany(n => n.Images)
            .HasForeignKey(a => a.NoteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}