using GigHub.Core;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace GigHub.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public HomeController (IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public ActionResult Index (string query = null)
		{
			var userId = User.Identity.GetUserId();

			var viewModel = new GigsViewModel
			{
				UpcomingGigs = _unitOfWork.Gigs.GetGigsForQuery(query),
				ShowActions = User.Identity.IsAuthenticated,
				Heading = "Upcoming Gigs",
				SearchTerm = query,
				Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId)
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