using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SocialPlatform.Core.Models;

namespace SocialPlatform.API.Models;

public class User 
{

    public int Id { get; set; }
 
    [MinLength(6, ErrorMessage = "Username must be atleast 6 characters.")]
    public string Username { get; set; } = string.Empty;

    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? Nickname { get; set; } = string.Empty;
    public string? ImageName { get; set; } = string.Empty;
    public string Name { get; set; }
    [NotMapped]
    [JsonIgnore]
    public IFormFile? ImageFile { get; set; }
    [NotMapped]
    [JsonIgnore]
    public string? ImageSrc { get; set; }
    public DateTime Birthdate { get; set; }
    public Pronouns Pronouns { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public List<User>? Following { get; set; } 
    public List<User>? Followers { get; set; }
    public IList<Notification> Notifications { get; set; }


}