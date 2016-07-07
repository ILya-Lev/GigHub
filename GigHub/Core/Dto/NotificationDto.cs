using System;
using GigHub.Core.Models;

namespace GigHub.Core.Dto
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