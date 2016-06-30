using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
	public class HomeController : Controller
	{
		private ApplicationDbContext _context;

		public HomeController ()
		{
			_context = new ApplicationDbContext();
		}

		public ActionResult Index (string query = null)
		{
			Func<Gig, bool> predicate = g =>
			{
				if (g.DateTime <= DateTime.Now || g.IsCanceled)
					return false;
				return string.IsNullOrWhiteSpace(query)
					|| g.Genre.Name.Contains(query)
					|| g.Artist.Name.Contains(query)
					|| g.Venue.Contains(query);
			};

			var upcomingGigs = _context.Gigs.Include(g => g.Artist)
											.Include(g => g.Genre)
											.Where(predicate);

			var userId = User.Identity.GetUserId();
			var attendances = _context.Attendances
									  .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
									  .ToLookup(a => a.GigId);

			var viewModel = new GigsViewModel
			{
				UpcomingGigs = upcomingGigs,
				ShowActions = User.Identity.IsAuthenticated,
				Heading = "Upcoming Gigs",
				SearchTerm = query,
				Attendances = attendances
			};

			return View("Gigs", viewModel);
		}

		public ActionResult About ()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact ()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}