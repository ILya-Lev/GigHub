﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GigHub.Models
{
	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser
	{
		[Required]
		//[MaxLength(255)]
		[StringLength(128)]
		public string Name { get; set; }

		public ICollection<Following> Followers { get; set; } = new Collection<Following>();
		public ICollection<Following> Followees { get; set; } = new Collection<Following>();
		public ICollection<UserNotification> UserNotifications { get; set; } = new Collection<UserNotification>();

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync (UserManager<ApplicationUser> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}

		public void Notify(Notification notification)
			=> UserNotifications.Add(new UserNotification(this, notification));
	}
}