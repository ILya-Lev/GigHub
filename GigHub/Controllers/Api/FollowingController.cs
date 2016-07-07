using GigHub.Core;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
	[Authorize]
	public class FollowingController : ApiController
	{
		private readonly IUnitOfWork _unitOfWork;

		public FollowingController (IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpPost]
		public IHttpActionResult Follow (FollowingDto dto)
		{
			var userId = User.Identity.GetUserId();
			if (_unitOfWork.Followers.GetFollowing(userId, dto.FolloweeId) != null)
				return BadRequest("Following already exists.");

			var following = new Following { FollowerId = userId, FolloweeId = dto.FolloweeId };
			
			_unitOfWork.Followers.Add(following);
			_unitOfWork.Complete();

			return Ok();
		}

		[HttpDelete]
		public IHttpActionResult DeleteFollowing(string followeeId)
		{
			var userId = User.Identity.GetUserId();
			var following = _unitOfWork.Followers.GetFollowing(userId, followeeId);
			if (following == null)
				return NotFound();

			_unitOfWork.Followers.Remove(following);
			_unitOfWork.Complete();

			return Ok(followeeId);
		}
	}
}
