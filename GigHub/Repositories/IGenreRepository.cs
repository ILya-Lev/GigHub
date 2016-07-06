using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
	public interface IGenreRepository
	{
		IReadOnlyList<Genre> GetGenres ();
	}
}