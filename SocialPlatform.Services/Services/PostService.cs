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

    public async Task<List<Post>> GetPostsByFollowingAsync(int userId)
    {
        return await _postRepository.GetPostsByFollowingAsync(userId);
    }

    public async Task<Post> LikeAPost(int userId, int postId)
    {
        return await _postRepository.LikeAPost(userId, postId);
    }

    public async Task<Post?> GetPostById(int id)
    {
        return await _postRepository.GetPostById(id);
    }

    public async Task<Post> UnlikeAPost(int userId, int postId)
    {
        return await _postRepository.UnlikeAPost(userId, postId);
    }

    public async Task<Post?> CommentAPost(int userId, int postId, string content)
    {
        return await _postRepository.CommentAPost(userId, postId, content);
    }
}