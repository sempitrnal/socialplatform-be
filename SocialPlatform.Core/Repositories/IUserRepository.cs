using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;

namespace SocialPlatform.Data.Repositories;

public interface IUserRepository
{
    Task<User> CreateUserAsync(UserDto request);
}