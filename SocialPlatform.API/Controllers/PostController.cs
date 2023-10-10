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

    [HttpGet]
    public async Task<IActionResult> GetPostsByFollowing(int userId)
    {
        var posts = await _postService.GetPostsByFollowingAsync(userId);
        return Ok(posts);
    }
    [HttpGet]
    public async Task<IActionResult> GetPostById(int id)
    {
        var post = await _postService.GetPostById(id);
        return Ok(post);
    }
    [HttpPost]
    public async Task<IActionResult> LikeAPost(int userId, int postId)
    {
        var result = await _postService.LikeAPost(userId, postId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> UnlikeAPost(int userId, int postId)
    {
        var result = await _postService.UnlikeAPost(userId, postId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CommentAPost(int userId, int postId, string content)
    {
        var result = await _postService.CommentAPost(userId, postId, content);
        return Ok(result);
    }
}