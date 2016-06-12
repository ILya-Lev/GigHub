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

		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public static ApplicationDbContext Create() => new ApplicationDbContext();
	}
}