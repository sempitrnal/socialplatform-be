using Microsoft.AspNetCore.Mvc;
using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;

namespace SocialPlatform.API.Services;

public interface IAuthService
{
    Task<string?> LoginAsync(UserAuthDto request);
    String CreateToken(User user);

}