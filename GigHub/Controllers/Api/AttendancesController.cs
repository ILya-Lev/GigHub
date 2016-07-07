using GigHub.Core;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
	[Authorize]
	public class AttendancesController : ApiController
	{
		private readonly IUnitOfWork _unitOfWork;

		public AttendancesController (IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpPost]
		//on runtime dto is null if $.ajax() is used maybe I'm using it wrong way
		public IHttpActionResult Attend (AttendanceDto dto)
		{
			var userId = User.Identity.GetUserId();
			if (_unitOfWork.Attendances.GetAttendance(dto.GigId, userId) != null)
				return BadRequest("The attendance already  exists");

			var attendance = new Attendance
			{
				GigId = dto.GigId,
				AttendeeId = userId
			};
			
			_unitOfWork.Attendances.Add(attendance);
			_unitOfWork.Complete();

			return Ok();
		}

		[HttpDelete]
		public IHttpActionResult DeleteAttendance(int gigId)
		{
			var userId = User.Identity.GetUserId();
			var att = _unitOfWork.Attendances.GetAttendance(gigId, userId);
			if (att == null)
				return NotFound();

			_unitOfWork.Attendances.Remove(att);
			_unitOfWork.Complete();

			return Ok(gigId);
		}
	}
}
