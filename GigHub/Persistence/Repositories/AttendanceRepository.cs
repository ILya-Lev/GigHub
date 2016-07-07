using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Linq;

namespace GigHub.Persistence.Repositories
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

		public void Add(Attendance att)
		{
			_context.Attendances.Add(att);
		}

		public void Remove(Attendance att)
		{
			_context.Attendances.Remove(att);
		}

		public ILookup<int, Attendance> GetFutureAttendances (string userId)
		{
			return _context.Attendances
				.Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
				.ToLookup(a => a.GigId);
		}
	}
}