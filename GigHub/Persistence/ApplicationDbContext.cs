using GigHub.Core.Models;
using GigHub.Persistence.EntityConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GigHub.Persistence
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		//to notify EF about new custom model class - Gig;
		//it also contains references to class Genre - so EF will handle it automatically
		public DbSet<Gig> Gigs { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Attendance> Attendances { get; set; }
		public DbSet<Following> Followings { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<UserNotification> UserNotifications { get; set; }

		public ApplicationDbContext ()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public static ApplicationDbContext Create () => new ApplicationDbContext();

		protected override void OnModelCreating (DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new GigConfiguration());
			modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
			modelBuilder.Configurations.Add(new UserNotificationConfiguration());
			modelBuilder.Configurations.Add(new AttendanceConfiguration());

			modelBuilder.Entity<Following>().HasKey(f => new { f.FollowerId, f.FolloweeId });
			modelBuilder.Entity<Genre>().Property(g => g.Name).IsRequired().HasMaxLength(255);
			modelBuilder.Entity<Notification>().Property(n => n.GigId).IsRequired();

			base.OnModelCreating(modelBuilder);
		}
	}
}