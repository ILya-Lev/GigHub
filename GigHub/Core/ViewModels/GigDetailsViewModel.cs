using System.Linq;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
	public class GigDetailsViewModel
	{
		public string ArtistId { get; }
		public string ArtistName { get; }
		public bool IsAuthorized { get; }
		public bool IsFollowing { get; }
		public bool IsAttending { get; }
		public string Venue { get; }
		public string Date { get; }
		public string Time { get; }

		public GigDetailsViewModel (Gig gig, string userId)
		{
			ArtistId = gig.ArtistId;
			ArtistName = gig.Artist.Name;
			IsAuthorized = userId != null;
			IsFollowing = IsAuthorized && gig.Artist.Followees.Any(f => f.FollowerId == userId);
			IsAttending = IsAuthorized && gig.Attendances.Any(a => a.AttendeeId == userId);
			Venue = gig.Venue;
			Date = gig.DateTime.ToString("d MMM");
			Time = gig.DateTime.ToString("HH:mm");
		}
	}
}