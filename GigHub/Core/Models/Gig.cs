using GigHub.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GigHub.Core.Models
{
	public class Gig
	{
		public int Id { get; private set; }

		public bool IsCanceled { get; private set; }

		// it's navigation prop after we've added an ArtistId prop
		public ApplicationUser Artist { get; set; }

		public string ArtistId { get; set; }

		public DateTime DateTime { get; set; }

		public string Venue { get; private set; }

		public Genre Genre { get; private set; }

		public byte GenreId { get; private set; }

		public ICollection<Attendance> Attendances { get; } = new Collection<Attendance>();

		public Gig ()
		{
		}

		public Gig (string artistId, GigFormViewModel viewModel)
		{
			ArtistId = artistId;
			DateTime = viewModel.DateTime();
			GenreId = viewModel.Genre;
			Venue = viewModel.Venue;
		}

		public void Cancel ()
		{
			IsCanceled = true;

			var notification = Notification.CreateCanceledNotification(this);
			foreach (ApplicationUser attendee in Attendances.Select(a => a.Attendee))
			{
				attendee.Notify(notification);
			}
		}

		public void Update (GigFormViewModel viewModel)
		{
			var notification = Notification.CreateUpdateNotification(this);

			DateTime = viewModel.DateTime();
			GenreId = viewModel.Genre;
			Venue = viewModel.Venue;

			foreach (ApplicationUser attendee in Attendances.Select(a => a.Attendee))
				attendee.Notify(notification);
		}
	}
}