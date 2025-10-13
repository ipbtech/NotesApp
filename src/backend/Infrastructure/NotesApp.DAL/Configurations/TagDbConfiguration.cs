using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApp.Domain.Entities;

namespace NotesApp.DAL.Configurations;

internal class TagDbConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(t => t.Name)
            .IsUnique();

        builder.HasIndex(t => t.UsageCount);
        builder.HasIndex(t => t.CreatedAtUtc);

        builder.HasMany(t => t.Subscriptions)
            .WithOne(s => s.Tag)
            .HasForeignKey(s => s.TagId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}