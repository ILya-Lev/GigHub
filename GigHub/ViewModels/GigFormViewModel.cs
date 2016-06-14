using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
	public class GigFormViewModel
	{
		[Required]
		public string Venue { get; set; }

		[Required]
		[FutureDate]
		public string Date { get; set; }

		[Required]
		[ValidTime]
		public string Time { get; set; }

		[Required]
		public byte Genre { get; set; }

		public IEnumerable<Genre> Genres { get; set; }

		// if it is a prop MVC tries to construct the VM instance in parameter of controller
		// but it could not be valid, so make it a method in order to avoid exception generation
		// while we expect the case when DateTime could not be correctly created
		public DateTime DateTime () => System.DateTime.Parse($"{Date} {Time}");
	}
}