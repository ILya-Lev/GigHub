using AutoMapper;
using GigHub.Dto;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
	[Authorize]
	public class NotificationsController : ApiController
	{
		private ApplicationDbContext Context { get; } = new ApplicationDbContext();

		public IEnumerable<NotificationDto> GetNewNotifications ()
		{
			var userId = User.Identity.GetUserId();
			var notifications = Context.UserNotifications
				.Where(un => un.UserId == userId && !un.IsRead)
				.Select(un => un.Notification)
				.Include(n => n.Gig.Artist)
				.ToList();

			return notifications.Select(Mapper.Map<Notification, NotificationDto>);
		}

		[HttpPost]
		public IHttpActionResult MarkAsRead ()
		{
			var userId = User.Identity.GetUserId();
			var notifications = Context.UserNotifications
				.Where(un => un.UserId == userId && !un.IsRead)
				.ToList();

			//if (!notifications.Any())
			//	return NotFound();

			notifications.ForEach(un => un.Read());

			Context.SaveChanges();
			return Ok();
		}
	}
}

/*
 //manual mapping
 n => new NotificationDto
			{
				DateTime = n.DateTime,
				Gig = new GigDto
				{
					Artist = new UserDto
					{
						Id = n.Gig.Artist.Id,
						Name = n.Gig.Artist.Name
					},
					DateTime = n.Gig.DateTime,
					//Genre = new GenreDto
					//{
					//	Id = n.Gig.Genre.Id,
					//	Name = n.Gig.Genre.Name
					//},
					Id = n.Gig.Id,
					IsCanceled = n.Gig.IsCanceled,
					Venue = n.Gig.Venue
				},
				OriginalDateTime = n.OriginalDateTime,
				OriginalVenue = n.OriginalVenue,
				Type = n.Type
			}
*/
