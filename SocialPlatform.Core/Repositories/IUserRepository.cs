using Microsoft.AspNetCore.Mvc;
using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;

namespace SocialPlatform.Data.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetUsersAsync();
    Task<List<User>> SearchUsers(string query);
    Task<User> CreateUserAsync(UserCreationDto request);
    Task<User> UpdateUserAsync(int id, [FromForm]User updatedUser);
    Task<User?> FindUserByUsernameAsync(string username);
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> FollowAUserAsync(string username, string userToFollow);
    Task<User?> UnfollowAUserAsync(string username, string userToUnfollow);
    bool UsernameExists(string username);
}