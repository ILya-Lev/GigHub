using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
	public interface INotificationRepository
	{
		IReadOnlyCollection<UserNotification> GetNotReadNotifications(string userId);
		IReadOnlyCollection<Notification> GetNotReadNotificationsWithArtits(string userId);
	}
}