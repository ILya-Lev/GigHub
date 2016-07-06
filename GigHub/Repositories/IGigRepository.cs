using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
	public interface IGigRepository
	{
		Gig GetGig (int gigId);
		Gig GetGigWithAttendees (int gigId);
		Gig GetGigWithFollowees (int gigId);
		IReadOnlyList<Gig> GetGigsUserAttending (string userId);
		IReadOnlyList<Gig> GetUpcomingGigsByArtist (string userId);
		void Add (Gig gig);
	}
}