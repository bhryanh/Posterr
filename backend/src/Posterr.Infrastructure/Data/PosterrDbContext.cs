using Microsoft.EntityFrameworkCore;
using Posterr.Domain.Entities;
using Posterr.Infrastructure.Data.Configurations;

namespace Posterr.Infrastructure.Data;

public class PosterrDbContext : DbContext
{
    public PosterrDbContext(DbContextOptions<PosterrDbContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());

        SeedData(modelBuilder);
    }
    
    public void SeedData(ModelBuilder modelBuilder)
    {
        var user1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var user2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var user3Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var user4Id = Guid.Parse("44444444-4444-4444-4444-444444444444");

        var baseDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<User>().HasData(
            new
            {
                Id = user1Id,
                UserName = "bhryan",
                CreatedAt = baseDate.AddDays(-30)
            },
            new
            {
                Id = user2Id,
                UserName = "barney",
                CreatedAt = baseDate.AddDays(-25)
            },
            new
            {
                Id = user3Id,
                UserName = "sergey",
                CreatedAt = baseDate.AddDays(-20)
            },
            new
            {
                Id = user4Id,
                UserName = "daisy",
                CreatedAt = baseDate.AddDays(-15)
            }
        );

        var post1Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var post2Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        var post3Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

        modelBuilder.Entity<Post>().HasData(
            new
            {
                Id = post1Id,
                Content = "Hello Posterr! This is my first post.",
                CreatedAt = baseDate.AddHours(-5),
                IsRepost = false,
                AuthorId = user1Id,
                OriginalPostId = (Guid?)null
            },
            new
            {
                Id = post2Id,
                Content = "Just joined Posterr! Excited to be here.",
                CreatedAt = baseDate.AddHours(-4),
                IsRepost = false,
                AuthorId = user2Id,
                OriginalPostId = (Guid?)null
            },
            new
            {
                Id = post3Id,
                Content = "This is a great platform!",
                CreatedAt = baseDate.AddHours(-3),
                IsRepost = false,
                AuthorId = user3Id,
                OriginalPostId = (Guid?)null
            },
            new
            {
                Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                Content = "Hello Posterr! This is my first post.",
                CreatedAt = baseDate.AddHours(-2),
                IsRepost = true,
                AuthorId = user4Id,
                OriginalPostId = post1Id
            }
        );
    }
}