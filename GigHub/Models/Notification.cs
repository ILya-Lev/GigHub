using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
	public class Notification
	{
		public int Id { get; private set; }

		public NotificationType Type { get; private set; }

		public DateTime DateTime { get; private set; }

		public DateTime? OriginalDateTime { get; private set; }

		public string OriginalVenue { get; private set; }

		[Required]
		public Gig Gig { get; private set; }

		protected Notification()
		{
		}

		private Notification(Gig gig, NotificationType type)
		{
			if (gig == null) throw new ArgumentNullException(nameof(gig));

			Type = type;
			Gig = gig;
			DateTime = DateTime.Now;
		}

		public static Notification CreateNewNotification(Gig gig)
		{
			return new Notification(gig, NotificationType.GigCreated);
		}

		public static Notification CreateUpdateNotification(Gig gig)
		{
			var notification = new Notification(gig, NotificationType.GigUpdated)
			{
				OriginalVenue = gig.Venue,
				OriginalDateTime = gig.DateTime
			};

			return notification;
		}

		public static Notification CreateCanceledNotification (Gig gig)
		{
			return new Notification(gig, NotificationType.GigCanceled);
		}
	}
}