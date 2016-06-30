using GigHub.Dto;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
	[Authorize]
	public class AttendancesController : ApiController
	{
		private ApplicationDbContext _context;

		public AttendancesController ()
		{
			_context = new ApplicationDbContext();
		}

		[HttpPost]
		//on runtime dto is null if $.ajax() is used maybe I'm using it wrong way
		public IHttpActionResult Attend (AttendanceDto dto)
		{
			var userId = User.Identity.GetUserId();

			if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId))
				return BadRequest("The attendance already  exists");

			var attendance = new Attendance
			{
				GigId = dto.GigId,
				AttendeeId = userId
			};
			_context.Attendances.Add(attendance);
			_context.SaveChanges();

			return Ok();
		}
	}
}
