using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchList.Interfaces;
using WatchList.Models;
using WatchList.Models.TMDBParse;

namespace WatchList.Controllers
{
    public class SearchController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ITVRepository _tVRepository;
        private readonly IGenreRepository _genreRepository;

        private ToTMDB toTMDB;
        public SearchController(IMovieRepository movieRepository, ITVRepository tVRepository, IGenreRepository genreRepository)
        {
            _movieRepository = movieRepository;
            toTMDB = new ToTMDB();
            _tVRepository = tVRepository;
            _genreRepository = genreRepository;
        }

        public async Task<IActionResult> Search(string query, int NumPage = 1) 
        {
            if(HttpContext.Session.GetString("query")==null || (query != null &&!HttpContext.Session.GetString("query").Equals(query)))
            HttpContext.Session.SetString("query", query);

            var tuple = toTMDB.Search(HttpContext.Session.GetString("query"), NumPage);
            TMDB_Model[] tv = tuple.Item1.Results;

            foreach (var series in tv)
            {
                foreach (var item in series.GenresId)
                {
                    Genre genre = await _genreRepository.GetByIdTMDBAsync(item);
                    if (genre != null)
                    series.GenresInText.Add(genre.genre_name);
                }
            }

            Pages pages = new Pages() { CurrentPage = NumPage, MaxPageTV = tuple.Item2 };

            ViewData["TVData"] = tv;
            ViewData["NumPage"] = pages;

            return View();

        }

    }
}
