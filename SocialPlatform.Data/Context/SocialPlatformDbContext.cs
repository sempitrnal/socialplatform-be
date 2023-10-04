using Microsoft.EntityFrameworkCore;
using SocialPlatform.API.Models;

namespace SocialPlatform.Data.Context;

public class SocialPlatformDbContext : DbContext
{
    public SocialPlatformDbContext(DbContextOptions<SocialPlatformDbContext> options) : base(options)
    {
    }



    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<PostLike> PostLikes { get; set; }
}