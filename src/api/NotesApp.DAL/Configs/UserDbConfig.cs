using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApp.Domain.Entities;

namespace NotesApp.DAL.Configs
{
    internal class UserDbConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(e => e.Email).HasMaxLength(255).IsRequired();
            builder.Property(e => e.UserName).HasMaxLength(150);
            builder.Property(e => e.PasswordHash).IsRequired();
            builder.Property(e => e.Role).HasColumnType("text").IsRequired();

            builder.HasIndex(e => e.Email).IsUnique();

            builder.HasOne(e => e.Avatar).WithOne(e => e.User)
                .HasForeignKey<Avatar>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.RefreshTokens).WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Notes).WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
