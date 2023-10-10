using Microsoft.AspNetCore.Mvc;
using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;

namespace SocialPlatform.API.Services;

public interface IAuthService
{
    Task<string?> LoginAsync(UserAuthDto request);
    string CreateToken(User user);
    Task<bool?> CheckPassword(UserAuthDto request);
}