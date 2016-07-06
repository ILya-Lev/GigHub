using GigHub.Models;
using System;
using System.Linq;

namespace GigHub.Repositories
{
	public class AttendanceRepository : IAttendanceRepository
	{
		private readonly ApplicationDbContext _context;

		public AttendanceRepository (ApplicationDbContext context)
		{
			_context = context;
		}

		public Attendance GetAttendance (int gigId, string userId)
		{
			return _context.Attendances.SingleOrDefault(a => a.GigId == gigId && a.AttendeeId == userId);
		}

		public ILookup<int, Attendance> GetFutureAttendances (string userId)
		{
			return _context.Attendances
				.Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
				.ToLookup(a => a.GigId);
		}
	}
}