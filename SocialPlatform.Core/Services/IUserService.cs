using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;

namespace SocialPlatform.API.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(UserDto user);
}