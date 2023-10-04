using Microsoft.AspNetCore.Mvc;
using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;
using SocialPlatform.API.Services;
using SocialPlatform.Data.Repositories;

namespace SocialPlatform.Services.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<List<Post>> GetPostsByUserAsync(int userId)
    {
        return await _postRepository.GetPostsByUserAsync(userId);
    }

    public async Task<Post> CreatePost([FromForm]PostCreationDto post)
    {
        return await _postRepository.CreatePost(post);
    }
}