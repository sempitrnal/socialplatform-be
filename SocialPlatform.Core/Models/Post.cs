using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SocialPlatform.API.Models;

public class Post
{
    public int Id { get; set; }

    public User? User { get; set; }
    
    public int UserId { get; set; }
    
    public string? Description { get; set; }

    public DateTime PostCreatedAt { get; set; }
    public string? ImageName { get; set; } = string.Empty;

    [NotMapped]
    [JsonIgnore]
    public IFormFile? ImageFile { get; set; }
    [NotMapped]
    [JsonIgnore]
    public string? ImageSrc { get; set; }

    public List<Comment>? Comments { get; set; }
    public List<PostLike>? Likes { get; set; } 
}