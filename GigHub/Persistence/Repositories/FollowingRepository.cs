﻿using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
	public class FollowingRepository : IFollowingRepository
	{
		private readonly ApplicationDbContext _context;

		public FollowingRepository (ApplicationDbContext context)
		{
			_context = context;
		}

		public Following GetFollowing (string userId, string artistId)
		{
			return _context.Followings.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == artistId);
		}

		public IReadOnlyList<ApplicationUser> GetFolloweesForUser (string userId)
		{
			return _context.Followings.Where(f => f.FollowerId == userId)
									  .Select(f => f.Followee)
									  .ToList();
		}

		public void Add (Following f)
		{
			_context.Followings.Add(f);
		}

		public void Remove(Following f)
		{
			_context.Followings.Remove(f);
		}
	}
}