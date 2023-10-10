using System;
using SocialPlatform.API.Models;

namespace SocialPlatform.Core.Models
{
    public class Notification
	{
		public int Id { get; set; }
		public User User { get; set; } 
		public int UserId { get; set; }
		public NotificationType NotificationType { get; set; }
		public string Content { get; set; } = string.Empty;
	}
}

