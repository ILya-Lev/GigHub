using AutoMapper;
using GigHub.Core;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebGrease.Css.Extensions;

namespace GigHub.Controllers.Api
{
	[Authorize]
	public class NotificationsController : ApiController
	{
		private readonly IUnitOfWork _unitOfWork;

		public NotificationsController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<NotificationDto> GetNewNotifications ()
		{
			var notifications = _unitOfWork.Notifications
									.GetNotReadNotificationsWithArtits(User.Identity.GetUserId());

			return notifications.Select(Mapper.Map<Notification, NotificationDto>);
		}

		[HttpPost]
		public IHttpActionResult MarkAsRead ()
		{
			var notifications = _unitOfWork.Notifications.GetNotReadNotifications(User.Identity.GetUserId());

			//if (!notifications.Any())
			//	return NotFound();

			notifications.ForEach(un => un.Read());

			_unitOfWork.Complete();
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
