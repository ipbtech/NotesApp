using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApp.Domain.Entity;

namespace NotesApp.DAL.Config
{
    public class UserDbConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(e => e.Email).HasMaxLength(255).IsRequired();
            builder.Property(e => e.UserName).HasMaxLength(255).IsRequired();
            builder.Property(e => e.Password).IsRequired();

            builder.HasOne(u => u.Avatar).WithOne(a => a.User)
                .HasForeignKey<Avatar>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
