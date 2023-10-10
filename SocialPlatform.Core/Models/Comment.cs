using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialPlatform.API.Models;

public class Comment
{
    [Key]
    public int Id { get; set; }
    
    public User User { get; set; }

    public int UserId { get; set; }
    public string Content { get; set; }


    public DateTime? CommentCreatedAt { get; set; }

}