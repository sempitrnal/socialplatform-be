using Microsoft.EntityFrameworkCore;
using SocialPlatform.API.Models;

namespace SocialPlatform.Data.Context;

public class SocalPlatformDbContext : DbContext
{
    public SocalPlatformDbContext(DbContextOptions<SocalPlatformDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
}