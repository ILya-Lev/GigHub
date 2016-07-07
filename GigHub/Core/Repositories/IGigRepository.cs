using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
	public interface IGigRepository
	{
		Gig GetGig (int gigId);
		Gig GetGigWithAttendees (int gigId);
		Gig GetGigWithFollowees (int gigId);
		IReadOnlyCollection<Gig> GetGigsForQuery(string query);
		IReadOnlyList<Gig> GetGigsUserAttending (string userId);
		IReadOnlyList<Gig> GetUpcomingGigsByArtist (string userId);
		void Add (Gig gig);
	}
}