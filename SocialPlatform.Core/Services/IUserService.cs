using Microsoft.AspNetCore.Mvc;
using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;

namespace SocialPlatform.API.Services;

public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<User> CreateUserAsync(UserCreationDto userAuth);   
    Task<User> UpdateUserAsync(int id, [FromForm]User updatedUser);
    Task<User?> FindUserByUsernameAsync(string username);
    Task<User?> GetUserByIdAsync(int id);
    Task<List<User>> SearchUsers(string query);
    Task<User?> FollowAUserAsync(string username, string userToFollow);
    Task<User?> UnfollowAUserAsync(string username, string userToUnfollow);
    Boolean UsernameExists(string username);
  
}