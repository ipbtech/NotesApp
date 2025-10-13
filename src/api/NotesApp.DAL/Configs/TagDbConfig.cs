using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApp.Domain.Entities;

namespace NotesApp.DAL.Configs
{
    internal class TagDbConfig : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();

            builder.HasIndex(t => new { t.UserId, t.Name }).IsUnique();

            builder.HasOne(e => e.User).WithMany(e => e.Tags)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
