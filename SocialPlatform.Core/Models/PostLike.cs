using System.ComponentModel.DataAnnotations.Schema;

namespace SocialPlatform.API.Models;

public class PostLike
{
    public int Id { get; set; }

    public User User { get; set; }
    public int UserId { get; set; }

    
}