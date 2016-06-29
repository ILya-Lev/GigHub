using GigHub.Models;
using System.Linq;

namespace GigHub.ViewModels
{
	public class GigDetailsViewModel
	{
		public string ArtistName { get; }
		public bool IsFollowing { get; }
		public string Venue { get; }
		public string Date { get; }
		public string Time { get; }

		public GigDetailsViewModel (Gig gig, string userId)
		{
			ArtistName = gig.Artist.Name;
			IsFollowing = gig.Attendances.Any(a => a.AttendeeId == userId);
			Venue = gig.Venue;
			Date = gig.DateTime.ToString("D MMM");
			Time = gig.DateTime.ToString("HH:mm");
		}
	}
}