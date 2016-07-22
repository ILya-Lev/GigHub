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
	public class GigRepositoryTests
	{
		private GigRepository _repository;
		private Mock<DbSet<Gig>> _mockGigs;
		private Mock<DbSet<Attendance>> _mockAttendance;

		[TestInitialize]
		public void TestInitialize ()
		{
			_mockGigs = new Mock<DbSet<Gig>>();
			_mockAttendance = new Mock<DbSet<Attendance>>();

			var mockContext = new Mock<IApplicationDbContext>();
			mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
			mockContext.SetupGet(c => c.Attendances).Returns(_mockAttendance.Object);
			
			_repository = new GigRepository(mockContext.Object);
		}

		[TestMethod]
		public void GetUpcomingGigsByArtist_GigsInThePast_ShouldNotBeReturned()
		{
			//arrange
			var gig = new Gig() {DateTime = DateTime.Now.AddDays(-1), ArtistId = "1"};
			_mockGigs.SetSource(new [] {gig});
			//act
			var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId);
			//assert
			gigs.Should().BeEmpty();
		}

		[TestMethod]
		public void GetUpcomingGigsByArtist_GigIsCanceled_ShouldNotBeReturned()
		{
			//arrange
			var gig = new Gig() {DateTime = DateTime.Now.AddDays(1), ArtistId = "1"};
			gig.Cancel();
			_mockGigs.SetSource(new []{gig});
			//act
			var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId);
			//assert
			gigs.Should().BeEmpty();
		}

		[TestMethod]
		public void GetUpcomingGigsByArtist_GigForAnotheAtrist_ShouldNotBeReturned()
		{
			//arrange
			var gig = new Gig() {DateTime = DateTime.Now.AddDays(1), ArtistId = "1"};
			_mockGigs.SetSource(new [] {gig});
			//act
			var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId + "qq");
			//assert
			gigs.Should().BeEmpty();
		}

		[TestMethod]
		public void GetUpcomingGigsByArtist_ValidConditions_ShouldReturnAGig()
		{
			//arrange
			var gig = new Gig() {DateTime = DateTime.Now.AddDays(1), ArtistId = "1"};
			_mockGigs.SetSource(new [] {gig});
			//act
			var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId);
			//assert
			gigs.Should().HaveCount(1);
			gigs.Should().HaveElementAt(0, gig);
		}

		[TestMethod]
		public void GetGigsUserAttending_NoGigsForTheUser_ShouldNotReturn()
		{
			//arrange
			var gig = new Gig{DateTime = DateTime.Today.AddDays(1), ArtistId = "1"};
			_mockGigs.SetSource(new [] {gig});

			var attendance = new Attendance {Gig = gig, AttendeeId = "2"};
			_mockAttendance.SetSource(new []{attendance});
			//act
			var gigs = _repository.GetGigsUserAttending(gig.ArtistId);
			//assert
			gigs.Should().BeEmpty();//why _repo.Attendances is empty ? - wrong mocking
		}
	}
}
