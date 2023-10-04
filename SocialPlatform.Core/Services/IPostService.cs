using Microsoft.AspNetCore.Mvc;
using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;

namespace SocialPlatform.API.Services;

public interface IPostService
{
    Task<List<Post>> GetPostsByUserAsync(int userId);
    Task<Post> CreatePost([FromForm] PostCreationDto post);
}