namespace SocialPlatform.API.Models.Dto;

public class UserCreationDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? Nickname { get; set; } = string.Empty;
    public DateTime Birthdate { get; set; }
    public Pronouns Pronouns { get; set; }
}