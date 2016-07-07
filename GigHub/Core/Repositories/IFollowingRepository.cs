using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
	public interface IFollowingRepository
	{
		Following GetFollowing (string userId, string artistId);
		IReadOnlyList<ApplicationUser> GetFolloweesForUser (string userId);
		void Add(Following f);
		void Remove(Following f);
	}
}