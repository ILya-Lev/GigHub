using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;

		public IGigRepository Gigs { get; }
		public IAttendanceRepository Attendances { get; }
		public IGenreRepository Genres { get; }
		public IFollowingRepository Followers { get; }
		public INotificationRepository Notifications { get; }

		public UnitOfWork (ApplicationDbContext context)
		{
			_context = context;
			Gigs = new GigRepository(context);
			Attendances = new AttendanceRepository(context);
			Genres = new GenreRepository(context);
			Followers = new FollowingRepository(context);
			Notifications = new NotificationRepository(context);
		}

		public void Complete ()
		{
			_context.SaveChanges();
		}
	}
}