using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialPlatform.API.Models;

public class Comment
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Post { get; set; } = string.Empty;
    
    [Required]
    public User User { get; set; }
    
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public DateTime CommentCreatedAt { get; set; }

}