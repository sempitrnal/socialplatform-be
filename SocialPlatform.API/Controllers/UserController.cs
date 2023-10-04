using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;
using SocialPlatform.API.Services;

namespace SocialPlatform.API.Controllers;

[Route("/api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserCreationDto request)
    {
        var user = await _userService.FindUserByUsernameAsync(request.Username);
        if (user is not null)
            return BadRequest("Username exists.");
       
        return Ok( await _userService.CreateUserAsync(request));
    }

    [HttpPut("/api/user/{id}")]
    public async Task<IActionResult> UpdateUser(int id,[FromForm]User userToUpdate)
    {

        var user = await _userService.UpdateUserAsync(id, userToUpdate);
        return Ok(user);
    }   


    [HttpGet("/api/User/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user is null)
        {
            return BadRequest("User not found.");
        }

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        var user = await _userService.FindUserByUsernameAsync(username);
        if (user is null)
        {
            return BadRequest("User not found.");
        }

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet]
    public async Task<IActionResult> SearchUsers(string query)
    {
        var users = await _userService.SearchUsers(query);
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> FollowAUser(string username, string userToFollow)
    {
        var user = await _userService.FollowAUserAsync(username, userToFollow);
        if (user is null)
            return BadRequest("failed");
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> UnfollowAUser(string username, string userToUnfollow)
    {
        var user = await _userService.UnfollowAUserAsync(username, userToUnfollow);
        if (user is null)
            return BadRequest("failed");
        return Ok(user);
    }
}