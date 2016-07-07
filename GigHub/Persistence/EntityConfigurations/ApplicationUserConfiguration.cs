using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfigurations
{
	public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
	{
		public ApplicationUserConfiguration ()
		{
			Property(u => u.Name).IsRequired().HasMaxLength(128);
			HasMany(u => u.Followers).WithRequired(u => u.Followee).WillCascadeOnDelete(false);
			HasMany(u => u.Followees).WithRequired(u => u.Follower).WillCascadeOnDelete(false);
		}
	}
}