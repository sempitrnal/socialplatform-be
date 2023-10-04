using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialPlatform.API.Models;
using SocialPlatform.API.Models.Dto;
using SocialPlatform.Data.Context;

namespace SocialPlatform.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SocialPlatformDbContext _socialPlatformDbContext;
    private readonly IWebHostEnvironment _hostEnvironment;

    public UserRepository(SocialPlatformDbContext socialPlatformDbContext, IWebHostEnvironment hostEnvironment)
    {
        _socialPlatformDbContext = socialPlatformDbContext;
        _hostEnvironment = hostEnvironment;
    }

    public async  Task<List<User>> GetUsersAsync()
    {
        var users = await _socialPlatformDbContext.Users.Include(u=>u.Following).ToListAsync();
        return users;
    }

    public async Task<User> CreateUserAsync(UserCreationDto request)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        int pronouns = (int)request.Pronouns;
        User user = new User();
        user.Username = request.Username;
        user.PasswordHash = passwordHash;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Nickname = request.Nickname ?? null;
        user.Pronouns = request.Pronouns;
        user.Birthdate = request.Birthdate;
        user.Name = $"{request.FirstName} {request.LastName}";
         _socialPlatformDbContext.Users.Add(user);
         await _socialPlatformDbContext.SaveChangesAsync();
         return user;
    }

    public async Task<User> UpdateUserAsync(int id,[FromForm] User updatedUser)
    {
      
       
        if (updatedUser.ImageName != null)
        {
            
            updatedUser.ImageName = await SaveImage(updatedUser.ImageFile, updatedUser.Username);
            
            
        }
        Console.WriteLine(updatedUser.ImageName);
        _socialPlatformDbContext.Users.Update(updatedUser);
        await _socialPlatformDbContext.SaveChangesAsync();
        _socialPlatformDbContext.Entry(updatedUser).State = EntityState.Modified;
        try
        {

        }
        catch (DbUpdateConcurrencyException)
        {
            if (UserExists(id))
            {
                return null;
            }
            else
            {
                throw;
            }
        }

        return updatedUser;
    }

    // public async Task<User> UpdateUserAsync(int id, User user)
    // {
    //     _socialPlatformDbContext.Entry(user).State = EntityState.Modified;
    //     await _socialPlatformDbContext.SaveChangesAsync();
    //     return user;
    // }


    public async  Task<User?> FindUserByUsernameAsync(string username)
    {
        var user = await _socialPlatformDbContext.Users
            .Include(u=>u.Following)
            .Include(u=>u.Followers)
        
            .FirstOrDefaultAsync(u =>
                u.Username == username);
        return user;
    }
    public async Task<List<User>>  SearchUsers(string query)
    {
        var filteredUsers = await _socialPlatformDbContext.Users
            .Where(u => u.Name.Contains(query))
            .ToListAsync();

        return filteredUsers;
    }


    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _socialPlatformDbContext.Users
            .Include(u=>u.Followers)
            .Include(u=>u.Following)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> FollowAUserAsync(string username, string userToFollow)
    {
        var user = await FindUserByUsernameAsync(username);
        var user2Follow = await FindUserByUsernameAsync(userToFollow);
        if (user is null)
            return null;
        if (user2Follow is null)
            return null;
        
        user.Following?.Add(user2Follow);
        user2Follow.Followers?.Add(user);   
        await _socialPlatformDbContext.SaveChangesAsync();
        return user;
    }
    public async Task<User?> UnfollowAUserAsync(string username, string userToUnfollow)
    {
        var user = await FindUserByUsernameAsync(username);
        var user2Unfollow = await FindUserByUsernameAsync(userToUnfollow);
        if (user is null)
            return null;
        if (user2Unfollow is null)
            return null;
        
        user.Following?.Remove(user2Unfollow);
        user2Unfollow.Followers?.Remove(user);   
        await _socialPlatformDbContext.SaveChangesAsync();
        return user;
    }
    public bool UsernameExists(string username)
    {
        var user = FindUserByUsernameAsync(username);
        if (user.Result is null)
            return false;
        return true;
    }

    private bool UserExists(int id) => _socialPlatformDbContext.Users.Any(e => e.Id == id);
    public async Task<string> SaveImage(IFormFile imageFile, string name)
    {
            
        string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
        imageName = name + "-" + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
        var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, $"Images/users", imageName);
        using (var fileStream = new FileStream(imagePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        }
        Console.WriteLine(imageName);
        return imageName;
    }

    public void DeleteImage(string imageName)
    {
        var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images/users", imageName);
        if (System.IO.File.Exists(imagePath))
            System.IO.File.Delete(imagePath);
    }
}