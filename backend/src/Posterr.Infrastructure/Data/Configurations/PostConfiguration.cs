using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posterr.Domain.Entities;

namespace Posterr.Infrastructure.Data.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Content)
            .IsRequired()
            .HasMaxLength(777);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.IsRepost)
            .IsRequired();

        builder.Property(p => p.AuthorId)
            .IsRequired();

        builder.Property(p => p.OriginalPostId)
            .IsRequired(false);

        builder.HasOne(p => p.OriginalPost)
            .WithMany(p => p.Reposts)
            .HasForeignKey(p => p.OriginalPostId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}