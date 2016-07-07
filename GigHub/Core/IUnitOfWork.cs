using GigHub.Core.Repositories;

namespace GigHub.Core
{
	public interface IUnitOfWork
	{
		IGigRepository Gigs { get; }
		IAttendanceRepository Attendances { get; }
		IGenreRepository Genres { get; }
		IFollowingRepository Followers { get; }
		INotificationRepository Notifications { get; }
		void Complete ();
	}
}