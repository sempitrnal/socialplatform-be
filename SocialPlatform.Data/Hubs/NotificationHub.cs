using System;
using Microsoft.AspNetCore.SignalR;
using SocialPlatform.Data.Repositories;

namespace SocialPlatform.API.Hubs
{
    public class NotificationHub : Hub
	{

        private readonly IUserRepository _userRepository;

        public NotificationHub(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private static readonly Dictionary<string, string> userConnections = new Dictionary<string, string>();

        public async Task OnConnectedAsync(string userId)
        {
            Console.WriteLine("HELLOWWWWWWWWWORLDDDDDDDDDDDDD");
            userConnections[userId] = Context.ConnectionId;
            Console.WriteLine(Context.ConnectionId);
            await Clients.All.SendAsync("UserJoined", userId);

        }
        //public async Task OnConnectedAsync(string user)
        //{
        //    Console.WriteLine(user);


        //    await Clients.All.SendAsync("UserJoined", user);
        //}

        public async Task NotifyLike(int userId, int postId, string postUserId)
        {
            Console.WriteLine("LIKEEEEEEEEEEEEEEEEEEEEEEEEeee");
            Console.WriteLine("user connections: ");
            Console.WriteLine(userConnections);
            foreach(var kvp in userConnections)
            {
                Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
            }
            var user = await _userRepository.GetUserByIdAsync(userId);
            if(userConnections.TryGetValue(postUserId, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveLikeNotification", $"{user.Name} liked your post!", postId);
            }
                // Logic to handle the "Like" notification
      
        }
    }
}

