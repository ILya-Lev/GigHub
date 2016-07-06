using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
	public interface IFollowingRepository
	{
		Following GetFollowing (string userId, string artistId);
		IReadOnlyList<ApplicationUser> GetFolloweesForUser (string userId);
	}
}