using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialPlatform.API.Models.Dto;

public class PostCreationDto
{
    public int UserId { get; set; }
    
    public string? Description { get; set; }

    public DateTime? PostCreatedAt { get; set; } = DateTime.Now;
    public string? ImageName { get; set; } = string.Empty;

    [NotMapped]
    [JsonIgnore]
    public IFormFile? ImageFile { get; set; }
    [NotMapped]
    [JsonIgnore]
    public string? ImageSrc { get; set; }
}