using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialPlatform.API.Hubs;
using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;
using SocialPlatform.Core.Models.Dto;
using SocialPlatform.Data.Context;

namespace SocialPlatform.Data.Repositories;

public class PostRepository : IPostRepository
{
    private readonly SocialPlatformDbContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IUserRepository _userRepository;
    private readonly IHubContext<NotificationHub> _hubContext;
    public PostRepository(SocialPlatformDbContext context, IUserRepository userRepository, IWebHostEnvironment hostEnvironment, IHubContext<NotificationHub> hubContext)
    {
        _context = context;
        _userRepository = userRepository;
        _hostEnvironment = hostEnvironment;
        _hubContext = hubContext;
    }

    public async Task<List<Post>> GetPostsByUserAsync(int userId)
    {
        var posts = await _context.Posts.Where(p => p.UserId == userId)
            .Include(p => p.User)
            .Include(p=>p.Likes)
            .Include(p=>p.Comments)
            .ThenInclude(c=>c.User)
            .OrderByDescending(p => p.PostCreatedAt)
            .ToListAsync();
        return posts;
    }
    public async Task<Post?> GetPostById(int id)
    {
        var post = await _context.Posts
            .Include(p => p.User)
            .Include(p=>p.Likes)
            .Include(p=>p.Comments)
            .FirstOrDefaultAsync(p=>p.Id == id);
       
        return post;
    }
    public  List<Post> GetPostsByUser(int userId)
    {
        var posts =  _context.Posts.Where(p => p.UserId == userId)
            .Include(p => p.User)
            .Include(p=>p.Likes)
            .Include(p=>p.Comments)
            .OrderByDescending(p => p.PostCreatedAt)
            .ToList();
        return posts;
    }
    public async Task<List<Post>> GetPostsByFollowingAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user is null)
            return null;

        var followings = user.Following;
        List<Post> posts = new List<Post>();

        foreach(var u in followings)
        {
            var userposts = GetPostsByUser(u.Id);
            posts.AddRange(userposts);

        }
        var postsCurrentUser = GetPostsByUser(user.Id);
        posts.AddRange(postsCurrentUser);
        return posts.OrderByDescending(post=>post.PostCreatedAt).ToList();

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
    public async Task<Post> LikeAPost(int userId, int postId)
    {

        var user = await _userRepository.GetUserByIdAsync(userId);
        var post = await GetPostById(postId);
        PostLike like = new PostLike
        {
            User = user,
            UserId = user.Id
        };
       if(post is not null)
        {
            post.Likes?.Add(like);
            await _context.SaveChangesAsync();
          
            return post;
        }

        return null;

       
        

    }


   
    public async Task<Post> UnlikeAPost(int userId, int postId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        var post = await GetPostById(postId);
        var like = post.Likes.FirstOrDefault(pl => pl.UserId == user.Id);
        if (post is not null)
        {
            post.Likes?.Remove(like);
            _context.PostLikes.Remove(like);
            await _context.SaveChangesAsync();
            return post;
        }
        return null;
    }

    public async Task<Post?> CommentAPost(int userId, int postId, string content)
    {
        var post = await GetPostById(postId);
        var user = await _userRepository.GetUserByIdAsync(userId);
        if(user is not null)
        {
            var comment = new Comment
            {
                CommentCreatedAt = DateTime.Now,
                Content = content,
                UserId = userId,
                User = user,

            };

            post.Comments.Add(comment);
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
        return imageName;
    }

    public void DeleteImage(string imageName)
    {
        var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images/posts", imageName);
        if (System.IO.File.Exists(imagePath))
            System.IO.File.Delete(imagePath);
    }


}