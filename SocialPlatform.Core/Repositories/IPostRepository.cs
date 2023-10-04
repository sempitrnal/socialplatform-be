using Microsoft.AspNetCore.Mvc;
using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;

namespace SocialPlatform.Data.Repositories;

public interface IPostRepository
{
     Task<List<Post>> GetPostsByUserAsync(int userId);
     Task<Post?> CreatePost(PostCreationDto post);
}