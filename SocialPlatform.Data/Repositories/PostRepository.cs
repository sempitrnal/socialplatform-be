using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;
using SocialPlatform.Data.Context;

namespace SocialPlatform.Data.Repositories;

public class PostRepository : IPostRepository
{
    private readonly SocialPlatformDbContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IUserRepository _userRepository;
    public PostRepository(SocialPlatformDbContext context, IUserRepository userRepository, IWebHostEnvironment hostEnvironment)
    {
        _context = context;
        _userRepository = userRepository;
        _hostEnvironment = hostEnvironment;
    }

    public async Task<List<Post>> GetPostsByUserAsync(int userId)
    {
        var posts = await _context.Posts.Where(p => p.UserId == userId)
            .Include(p=>p.User)
            .OrderByDescending(p=>p.PostCreatedAt)
            .ToListAsync();
        return posts;
    }

    public async Task<Post?> CreatePost(PostCreationDto postDto)
    {
        var user = await _userRepository.GetUserByIdAsync(postDto.UserId);
       if(user is not null)
        {
            var post = new Post
            {
                User = user,
                ImageName = postDto.ImageFile != null ? await SaveImage(postDto.ImageFile, user.Username) : null,
                UserId = postDto.UserId,
                Description = postDto.Description,
                PostCreatedAt = DateTime.Now

            };
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }
        return null;
         
    }

    public async Task<string> SaveImage(IFormFile imageFile, string name)
    {

        string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
        imageName = name + "-" + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
        var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, $"Images/posts", imageName);
        using (var fileStream = new FileStream(imagePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        }
        Console.WriteLine(imageName);
        return imageName;
    }

    public void DeleteImage(string imageName)
    {
        var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images/posts", imageName);
        if (System.IO.File.Exists(imagePath))
            System.IO.File.Delete(imagePath);
    }
}