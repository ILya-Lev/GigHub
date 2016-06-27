using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GigHub.Models
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
			modelBuilder.Entity<Attendance>()
				.HasRequired(a => a.Gig)
				.WithMany(g => g.Attendances)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ApplicationUser>()
				.HasMany(u => u.Followers)
				.WithRequired(u => u.Followee)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ApplicationUser>()
				.HasMany(u => u.Followees)
				.WithRequired(u => u.Follower)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<UserNotification>()
				.HasRequired(un => un.User)
				.WithMany(u => u.UserNotifications)
				.WillCascadeOnDelete(false);

			base.OnModelCreating(modelBuilder);
		}
	}
}