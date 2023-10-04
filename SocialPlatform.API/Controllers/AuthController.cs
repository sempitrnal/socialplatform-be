using Microsoft.AspNetCore.Mvc;
using SocialPlatform.API.Models.Dto;
using SocialPlatform.API.Services;

namespace SocialPlatform.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserAuthDto request)
    {
        var user = await _authService.LoginAsync(request);
        Console.WriteLine("hello world");
        if (user is null)
            return BadRequest("Invalid credentials.");
        return Ok(user);
    }
    

   
}