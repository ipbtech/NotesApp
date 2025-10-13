using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Entities.Base;

namespace NotesApp.DAL.Configurations;

internal class SubscriptionDbConfiguration : IEntityTypeConfiguration<BaseSubscription>
{
    public void Configure(EntityTypeBuilder<BaseSubscription> builder)
    {
        builder.ToTable("Subscription");

        builder.HasDiscriminator<string>("Type")
            .HasValue<UserSubscription>("User")
            .HasValue<TagSubscription>("Tag");

        builder.HasOne(c => c.User)
            .WithMany(u => u.Subscriptions)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}