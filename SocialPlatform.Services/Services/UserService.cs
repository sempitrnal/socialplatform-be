using Microsoft.AspNetCore.Mvc;
using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;
using SocialPlatform.API.Services;
using SocialPlatform.Data.Repositories;

namespace SocialPlatform.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _userRepository.GetUsersAsync();
    }

    public async Task<User> CreateUserAsync(UserCreationDto request)
    {
        return await _userRepository.CreateUserAsync(request);
    }

    public async Task<User> UpdateUserAsync(int id, [FromForm]User updatedUser)
    {
        return await _userRepository.UpdateUserAsync(id, updatedUser);
    }
    
    public async Task<User?> FindUserByUsernameAsync(string username)
    {
        return await _userRepository.FindUserByUsernameAsync(username);
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<User?> FollowAUserAsync(string username, string userToFollow)
    {
        return await _userRepository.FollowAUserAsync(username, userToFollow);
    }

    public async Task<User?> UnfollowAUserAsync(string username, string userToUnfollow)
    {
        return await _userRepository.UnfollowAUserAsync(username, userToUnfollow);
    }

    public bool UsernameExists(string username)
    {
        return _userRepository.UsernameExists(username);
    }

    public Task<List<User>> SearchUsers(string query)
    {
        return _userRepository.SearchUsers(query);
    }
}