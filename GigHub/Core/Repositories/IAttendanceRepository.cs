using GigHub.Core.Models;
using System.Linq;

namespace GigHub.Core.Repositories
{
	public interface IAttendanceRepository
	{
		Attendance GetAttendance(int gigId, string userId);
		void Add(Attendance att);
		void Remove(Attendance att);
		ILookup<int, Attendance> GetFutureAttendances (string userId);
	}
}