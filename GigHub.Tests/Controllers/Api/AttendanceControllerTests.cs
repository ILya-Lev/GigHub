using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
	[TestClass]
	public class AttendanceControllerTests
	{
		private AttendancesController _controller;
		private Mock<IAttendanceRepository> _mockRepository;
		private string _userId;

		[TestInitialize]
		public void TestInitialize()
		{
			_mockRepository = new Mock<IAttendanceRepository>();

			var mockUoW = new Mock<IUnitOfWork>();
			mockUoW.Setup(u => u.Attendances).Returns(_mockRepository.Object);

			_controller = new AttendancesController(mockUoW.Object);
			_userId = "1";
			_controller.MockCurrentUser(_userId, "user1@domain.com");
		}

		[TestMethod]
		public void Attend_TryCreateAlreadyExistingAttendance_ShouldReturnBadRequest()
		{
			var attendance = new Attendance();
			_mockRepository.Setup(r => r.GetAttendance(It.IsAny<int>(), _userId)).Returns(attendance);

			var result = _controller.Attend(new AttendanceDto {GigId = 1});

			result.Should().BeOfType<BadRequestErrorMessageResult>();
		}
	}
}
