namespace SocialPlatform.API.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public List<User>? Following { get; set; } = new List<User>();
    public List<User>? Followers { get; set; } = new List<User>();

}