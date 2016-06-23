using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
	[Authorize]
	public class GigsController : ApiController
	{
		private readonly ApplicationDbContext _context;

		public GigsController ()
		{
			_context = new ApplicationDbContext();
		}

		[HttpDelete]
		//public IHttpActionResult Cancel (GigsDto dto)
		public IHttpActionResult Cancel (int id)
		{
			var userId = User.Identity.GetUserId();
			var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

			if (gig.IsCanceled)
				return NotFound();

			gig.IsCanceled = true;

			var notification = new Notification
			{
				DateTime = DateTime.Now,
				Gig = gig,
				Type = NotificationType.GigCanceled
			};

			var attendees = _context.Attendances.Where(a => a.GigId == gig.Id).Select(a => a.Attendee).ToList();

			var userNotifications = attendees.Select(a => new UserNotification
			{
				Notification = notification,
				User = a
			}).ToList();

			_context.UserNotifications.AddRange(userNotifications);

			_context.SaveChanges();
			return Ok();
		}
	}
}
