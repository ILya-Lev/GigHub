using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
	public class GigsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public GigsController ()
		{
			_context = new ApplicationDbContext();
		}

		[Authorize]
		public ActionResult Create ()
		{
			var viewModel = new GigFormViewModel
			{
				Genres = _context.Genres.ToList(),
				Heading = "Add a Gig"
			};
			return View("GigForm", viewModel);
		}

		[Authorize]
		public ActionResult Edit (int id)
		{
			var userId = User.Identity.GetUserId();
			var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

			var viewModel = new GigFormViewModel
			{
				Id = id,
				Genres = _context.Genres.ToList(),
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
				viewModel.Genres = _context.Genres.ToList();
				return View("GigForm", viewModel);
			}

			var gig = new Gig(artistId: User.Identity.GetUserId(), viewModel: viewModel);

			_context.Gigs.Add(gig);
			_context.SaveChanges();
			return RedirectToAction("Mine", "Gigs");
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Update (GigFormViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Genres = _context.Genres.ToList();
				return View("GigForm", viewModel);
			}

			var userId = User.Identity.GetUserId();
			var gig = _context.Gigs
							  .Include(g => g.Attendances.Select(a => a.Attendee))
							  .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

			gig.Update(viewModel);

			_context.SaveChanges();
			return RedirectToAction("Mine", "Gigs");
		}

		[Authorize]
		public ActionResult Mine ()
		{
			var userId = User.Identity.GetUserId();
			var gigs = _context.Gigs.Where(g => g.ArtistId == userId
											&& g.DateTime > DateTime.Now
											&& !g.IsCanceled)
									.Include(g => g.Genre)
									.ToList();

			return View("Mine", gigs);
		}

		[Authorize]
		public ActionResult Attending ()
		{
			var userId = User.Identity.GetUserId();
			var gigs = _context.Attendances.Where(a => a.AttendeeId == userId).Select(a => a.Gig)
											.Include(g => g.Artist).Include(g => g.Genre).ToList();
			var gigsViewModel = new GigsViewModel
			{
				UpcomingGigs = gigs,
				ShowActions = true,
				Heading = "Gigs I'm Attending"
			};
			return View("Gigs", gigsViewModel);
		}

		[Authorize]
		public ActionResult Following ()
		{
			var userId = User.Identity.GetUserId();
			var followings = _context.Followings.Where(f => f.FollowerId == userId)
												.Select(f => f.Followee)
												.ToList();
			return View("Followings", followings);
		}
	}
}