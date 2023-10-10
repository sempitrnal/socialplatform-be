using Microsoft.AspNetCore.Mvc;
using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;

namespace SocialPlatform.Data.Repositories;

public interface IPostRepository
{
     Task<List<Post>> GetPostsByUserAsync(int userId);
     Task<Post?> CreatePost(PostCreationDto post);
    Task<List<Post>> GetPostsByFollowingAsync(int userId);
    Task<Post> LikeAPost(int userId, int postId);
    Task<Post> UnlikeAPost(int userId, int postId);
    Task<Post> CommentAPost(int userId, int postId, string content);
    Task<Post?> GetPostById(int id);
}