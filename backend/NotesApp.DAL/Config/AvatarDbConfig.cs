using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApp.Domain.Entity;

namespace NotesApp.DAL.Config
{
    public class AvatarDbConfig : IEntityTypeConfiguration<Avatar>
    {
        public void Configure(EntityTypeBuilder<Avatar> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(e => e.Name).HasMaxLength(255).IsRequired();
            builder.Property(e => e.FileExtension).HasMaxLength(10).IsRequired();
        }
    }
}
