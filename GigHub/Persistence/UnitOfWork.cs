using GigHub.Models;
using GigHub.Repositories;

namespace GigHub.Persistence
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;

		public IGigRepository Gigs { get; }
		public IAttendanceRepository Attendances { get; }
		public IGenreRepository Genres { get; }
		public IFollowingRepository Followers { get; }

		public UnitOfWork (ApplicationDbContext context)
		{
			_context = context;
			Gigs = new GigRepository(context);
			Attendances = new AttendanceRepository(context);
			Genres = new GenreRepository(context);
			Followers = new FollowingRepository(context);
		}

		public void Complete ()
		{
			_context.SaveChanges();
		}
	}
}