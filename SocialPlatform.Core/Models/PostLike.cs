using System.ComponentModel.DataAnnotations.Schema;

namespace SocialPlatform.API.Models;

public class UserLike
{
    public int Id { get; set; }
    [NotMapped]
    public User User { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
}