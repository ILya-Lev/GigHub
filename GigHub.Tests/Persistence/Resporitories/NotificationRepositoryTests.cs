using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

namespace GigHub.Tests.Persistence.Resporitories
{
	[TestClass]
	public class NotificationRepositoryTests
	{
		// test method GetNewNotificationsFor
		private NotificationRepository _repository;
		private Mock<DbSet<UserNotification>> _mockNotifications;

		[TestInitialize]
		public void TestInitialize ()
		{
			_mockNotifications = new Mock<DbSet<UserNotification>>();

			var mockContext = new Mock<IApplicationDbContext>();
			mockContext.SetupGet(c => c.UserNotifications).Returns(_mockNotifications.Object);

			_repository = new NotificationRepository(mockContext.Object);
		}

		// mocked user notification collection is empty. why?
		[TestMethod]
		public void GetNotReadNotifications_NotificationIsRead_ShouldNotReturn ()
		{
			//arrange
			var user = new ApplicationUser {Name = "John"};
			var gig = new Gig() {ArtistId = "1", DateTime = DateTime.Today.AddDays(1)};
			var notification = Notification.CreateNewNotification(gig);
			var userNotification = new UserNotification(user, notification);
			//userNotification.Read();

			_mockNotifications.SetSource(new[] {userNotification});
			//act
			var notifications = _repository.GetNotReadNotifications(gig.ArtistId);
			//assert
			notifications.Should().BeEmpty();
		}
	}
}
