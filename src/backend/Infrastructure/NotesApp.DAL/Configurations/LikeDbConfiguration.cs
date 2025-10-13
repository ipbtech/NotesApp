using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Entities.Base;

namespace NotesApp.DAL.Configurations;

internal class LikeDbConfiguration : IEntityTypeConfiguration<BaseLike>
{
    public void Configure(EntityTypeBuilder<BaseLike> builder)
    {
        builder.ToTable("Like");
        
        builder.HasDiscriminator<string>("Type")
            .HasValue<NoteLike>("Note")
            .HasValue<CommentLike>("Comment");
        
        builder.HasOne(c => c.User)
            .WithMany(u => u.Likes)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}