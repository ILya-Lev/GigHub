using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using GigHub.Controllers;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
	public class GigFormViewModel
	{
		public int Id { get; set; }

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

		public string Heading { get; set; }

		public string Action
		{
			get
			{
				Expression<Func<GigsController, ActionResult>> update = c => c.Update(this);
				Expression<Func<GigsController, ActionResult>> create = c => c.Create(this);

				var action = Id == 0 ? create : update;
				return (action.Body as MethodCallExpression).Method.Name;
			}
		}

		// if it is a prop MVC tries to construct the VM instance in parameter of controller
		// but it could not be valid, so make it a method in order to avoid exception generation
		// while we expect the case when DateTime could not be correctly created
		public DateTime DateTime () => System.DateTime.Parse($"{Date} {Time}");
	}
}