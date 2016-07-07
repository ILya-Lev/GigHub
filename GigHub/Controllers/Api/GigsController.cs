using GigHub.Core;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
	[Authorize]
	public class GigsController : ApiController
	{
		private readonly IUnitOfWork _unitOfWork;

		public GigsController (IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpDelete]
		//public IHttpActionResult Cancel (GigsDto dto)
		public IHttpActionResult Cancel (int id)
		{
			var gig = _unitOfWork.Gigs.GetGigWithAttendees(id);
			if (gig.ArtistId != User.Identity.GetUserId() || gig.IsCanceled)
				return NotFound();

			gig.Cancel();
			
			_unitOfWork.Complete();
			return Ok();
		}
	}
}
