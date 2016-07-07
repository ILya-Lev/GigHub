using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;

namespace GigHub.Controllers
{
	public class GigsController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public GigsController (IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			//var context = new ApplicationDbContext();
			//_unitOfWork = new UnitOfWork(context);
		}

		[Authorize]
		public ActionResult Create ()
		{
			var viewModel = new GigFormViewModel
			{
				Genres = _unitOfWork.Genres.GetGenres(),
				Heading = "Add a Gig"
			};
			return View("GigForm", viewModel);
		}

		[Authorize]
		public ActionResult Edit (int id)
		{
			var gig = _unitOfWork.Gigs.GetGig(id);
			if (gig == null) return HttpNotFound();
			if (gig.ArtistId != User.Identity.GetUserId()) return new HttpUnauthorizedResult();

			var viewModel = new GigFormViewModel
			{
				Id = id,
				Genres = _unitOfWork.Genres.GetGenres(),
				Date = gig.DateTime.ToString("dd MMM yyyy"),
				Time = gig.DateTime.ToString("t"),
				Genre = gig.GenreId,
				Venue = gig.Venue,
				Heading = "Edit a Gig"
			};
			return View("GigForm", viewModel);
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create (GigFormViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Genres = _unitOfWork.Genres.GetGenres();
				return View("GigForm", viewModel);
			}

			var gig = new Gig(artistId: User.Identity.GetUserId(), viewModel: viewModel);

			_unitOfWork.Gigs.Add(gig);
			_unitOfWork.Complete();
			return RedirectToAction("Mine", "Gigs");
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Update (GigFormViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Genres = _unitOfWork.Genres.GetGenres();
				return View("GigForm", viewModel);
			}

			var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);

			if (gig == null) return HttpNotFound();
			if (gig.ArtistId != User.Identity.GetUserId()) return new HttpUnauthorizedResult();

			gig.Update(viewModel);

			_unitOfWork.Complete();
			return RedirectToAction("Mine", "Gigs");
		}

		[Authorize]
		public ActionResult Mine ()
		{
			var gigs = _unitOfWork.Gigs.GetUpcomingGigsByArtist(User.Identity.GetUserId());

			return View("Mine", gigs);
		}

		[Authorize]
		public ActionResult Attending ()
		{
			var userId = User.Identity.GetUserId();

			var gigsViewModel = new GigsViewModel
			{
				UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
				ShowActions = true,
				Heading = "Gigs I'm Attending",
				Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId)
			};

			return View("Gigs", gigsViewModel);
		}

		[Authorize]
		public ActionResult Following ()
		{
			var followings = _unitOfWork.Followers.GetFolloweesForUser(User.Identity.GetUserId());
			return View("Followings", followings);
		}

		[Authorize]
		[HttpPost]
		public ActionResult Search (GigsViewModel viewModel)
		{
			return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
		}

		public ActionResult Details (int id)
		{
			var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;

			var gig = _unitOfWork.Gigs.GetGigWithFollowees(id);
			if (gig == null) return HttpNotFound();

			return View("Details", new GigDetailsViewModel(gig, userId));
		}
	}
}