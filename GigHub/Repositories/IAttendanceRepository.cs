using System.Linq;
using GigHub.Models;

namespace GigHub.Repositories
{
	public interface IAttendanceRepository
	{
		Attendance GetAttendance(int gigId, string userId);
		ILookup<int, Attendance> GetFutureAttendances (string userId);
	}
}