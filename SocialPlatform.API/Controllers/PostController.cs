using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;
using SocialPlatform.API.Services;

namespace SocialPlatform.API.Controllers;

[Route("/api/[controller]/[action]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    public PostController(IPostService postService)
    {
        _postService = postService;
    }


    [HttpPost]
    public async Task<IActionResult> CreatePost([FromForm] PostCreationDto post)
    {
        var result = await _postService.CreatePost(post);
        return Ok(result);
    }

    [HttpGet("/api/posts/{id}")]
    public async Task<IActionResult> GetPostsByUser(int id)
    {
        var posts = await _postService.GetPostsByUserAsync(id);
        return Ok(posts);
    }
}