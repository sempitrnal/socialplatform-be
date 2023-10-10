using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;
using SocialPlatform.API.Services;
    using SocialPlatform.Data.Repositories;

namespace SocialPlatform.Services.Services;

public class AuthService : IAuthService
{

    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public AuthService(IUserRepository userRepository, IConfiguration configuration, IUserService userService)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _userService = userService;
    }

    public async Task<string?> LoginAsync(UserAuthDto request)
    {
        var user = await  _userService.FindUserByUsernameAsync(request.Username);
        if (user is null)
            return null;
        else if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return null;
        return CreateToken(user);

    }
    public async Task<bool?> CheckPassword(UserAuthDto request)
    {
        var user = await _userService.FindUserByUsernameAsync(request.Username);
        if (user is null)
            return null;
        else if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return false;
        return true;
    }
    public string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
           
            new("username", user.Username),
            new("userId", user.Id.ToString()),
         
            
            
        };
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("Token").Value!));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            
            signingCredentials: cred
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}