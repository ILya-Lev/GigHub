using GigHub.Models;
using System;

namespace GigHub.Dto
{
	public class NotificationDto
	{
		public NotificationType Type { get; set; }
		public DateTime DateTime { get; set; }
		public DateTime? OriginalDateTime { get; set; }
		public string OriginalVenue { get; set; }
		public GigDto Gig { get; set; }
	}
}