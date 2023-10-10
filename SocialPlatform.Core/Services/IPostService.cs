using Microsoft.AspNetCore.Mvc;
using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;

namespace SocialPlatform.API.Services;

public interface IPostService
{
    Task<List<Post>> GetPostsByUserAsync(int userId);
    Task<Post> CreatePost([FromForm] PostCreationDto post);
    Task<List<Post>> GetPostsByFollowingAsync(int userId);
    Task<Post> LikeAPost(int userId, int postId);
    Task<Post> UnlikeAPost(int userId, int postId);
    Task<Post?> GetPostById(int id);
    Task<Post?> CommentAPost(int userId, int postId, string content);
}