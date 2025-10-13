using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApp.Domain.Entities;

namespace NotesApp.DAL.Configurations;

internal class NoteDbConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(n => n.Content)
            .IsRequired();

        builder.Property(n => n.PrivacyStatus)
            .HasConversion<string>()
            .HasColumnType("text");

        builder.HasIndex(n => n.PrivacyStatus);
        builder.HasIndex(n => n.LikesCount);
        builder.HasIndex(n => n.CreatedAtUtc);

        builder.HasOne(n => n.User)
            .WithMany(u => u.Notes)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(n => n.Likes)
            .WithOne(nl => nl.Note)
            .HasForeignKey(nl => nl.NoteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(n => n.Tags).WithMany(t => t.Notes);
    }
}