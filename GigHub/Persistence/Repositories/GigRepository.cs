using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
	public class GigRepository : IGigRepository
	{
		private readonly IApplicationDbContext _context;

		public GigRepository (IApplicationDbContext context)
		{
			_context = context;
		}

		public Gig GetGig (int gigId)
		{
			return _context.Gigs.SingleOrDefault(g => g.Id == gigId);
		}

		public Gig GetGigWithAttendees (int gigId)
		{
			return _context.Gigs
						.Include(g => g.Attendances.Select(a => a.Attendee))
						.SingleOrDefault(g => g.Id == gigId);
		}

		public Gig GetGigWithFollowees (int gigId)
		{
			return _context.Gigs
						.Include(g => g.Genre)
						.Include(g => g.Artist.Followers)
						.Include(g => g.Attendances)
						.SingleOrDefault(g => g.Id == gigId);
		}

		public IReadOnlyCollection<Gig> GetGigsForQuery(string query)
		{
			Func<Gig, bool> predicate = g =>
			{
				if (g.DateTime <= DateTime.Now || g.IsCanceled)
					return false;
				return string.IsNullOrWhiteSpace(query)
					|| g.Genre.Name.Contains(query)
					|| g.Artist.Name.Contains(query)
					|| g.Venue.Contains(query);
			};

			return _context.Gigs.Include(g => g.Artist)
								.Include(g => g.Genre)
								.Where(predicate).ToList();
		}

		public IReadOnlyList<Gig> GetGigsUserAttending (string userId)
		{
			return _context.Attendances
						.Where(a => a.AttendeeId == userId)
						.Select(a => a.Gig)
						.Include(g => g.Artist)
						.Include(g => g.Genre)
						.ToList();
		}

		public IReadOnlyList<Gig> GetUpcomingGigsByArtist (string userId)
		{
			return _context.Gigs
						.Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCanceled)
						.Include(g => g.Genre)
						.ToList();
		}

		public void Add (Gig gig)
		{
			_context.Gigs.Add(gig);
		}
	}
}