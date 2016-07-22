using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
	public class NotificationRepository : INotificationRepository
	{
		private readonly IApplicationDbContext _context;

		public NotificationRepository(IApplicationDbContext context)
		{
			_context = context;
		}

		public IReadOnlyCollection<UserNotification> GetNotReadNotifications(string userId)
		{
			return _context.UserNotifications
				.Where(un => un.UserId == userId && !un.IsRead)
				.ToList();
		}

		public IReadOnlyCollection<Notification> GetNotReadNotificationsWithArtits(string userId)
		{
			return _context.UserNotifications
				.Where(un => un.UserId == userId && !un.IsRead)
				.Select(un => un.Notification)
				.Include(n => n.Gig.Artist)
				.ToList();
		} 
	}
}