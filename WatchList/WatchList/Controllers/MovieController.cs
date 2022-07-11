using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using WatchList.Interfaces;
using WatchList.Models;
using WatchList.Models.TMDBParse;

namespace WatchList.Controllers
{
    public class MovieController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMovieRepository _movieRepository;
        ToTMDB toTMDB;
        public MovieController(IGenreRepository genreRepository, IMovieRepository movieRepository)
        {
            _genreRepository = genreRepository;
            _movieRepository = movieRepository;
            toTMDB = new ToTMDB();
        }

        [HttpGet]
        public async Task<ActionResult> TopMovie(int NumPage = 1)
        {
            var tuple=toTMDB.CallTopMovieAPI(NumPage);
            TMDB_Model[] movies = tuple.Item1;

            foreach (var movie in movies)
            {
                foreach (var item in movie.GenresId)
                {
                    Genre genre = await _genreRepository.GetByIdTMDBAsync(item);
                    movie.GenresInText.Add(genre.genre_name);
                }
            }

            Pages pages = new Pages() { CurrentPage = NumPage, MaxPageMovie=tuple.Item2-4 };

            ViewData["Data"] = movies;
            ViewData["NumPage"] = pages;

            return View("TopMovie");
        }

        public async Task<ActionResult> MoreInfoAboutMovie(int id)
        {
            TMDB_Model datalist = toTMDB.GetMoreAboutMovieById(id);

            List<Movie> moviesBuff = new List<Movie>();
            moviesBuff.AddRange(_movieRepository.GetMovieFromList(Convert.ToInt32(HttpContext.Session.GetString("userId"))));
            TMDB_Images tMDB_Images = new TMDB_Images();
            tMDB_Images.backdrops = toTMDB.GetImagesFromMovie(id).backdrops;
            tMDB_Images.results = toTMDB.GetVideosFromMovie(id).results;


            TMDB_Model[] movies = toTMDB.SimilarMovie(id);

            foreach (var movie in movies)
            {
                foreach (var item in movie.GenresId)
                {
                    Genre genre = await _genreRepository.GetByIdTMDBAsync(item);
                    movie.GenresInText.Add(genre.genre_name);
                }
            }


            ViewData["ListMovieFromDB"] = moviesBuff;
            ViewData["Data"] = datalist;
            ViewData["ImagesAndVideo"] = tMDB_Images;
            ViewData["SimilarMovie"] = movies;


            return View();
        }

        [HttpPost]
        public IActionResult AddMovieToList(int id)
        {
            if (HttpContext.Session.GetString("userId") != null)
            {
                Movie movie_toAdd = new Movie { idTMDB = id, isWatched = false, raiting = 0, idUser = Convert.ToInt32(HttpContext.Session.GetString("userId")) };
                _movieRepository.Add(movie_toAdd);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }
        }

        public IActionResult ToWatched(int id)
        {
            Movie movie = _movieRepository.GetMovieById(id, Convert.ToInt32(HttpContext.Session.GetString("userId")));
            movie.isWatched = true;
            _movieRepository.Update(movie);
            return RedirectToAction("ListPage", "Account");
        }

        public IActionResult Rate(int movieId, int rate)
        {
            Movie movie= _movieRepository.GetMovieById(movieId, Convert.ToInt32(HttpContext.Session.GetString("userId")));
            movie.raiting = rate;
            _movieRepository.Update(movie);
            return RedirectToAction("ListPage", "Account");
        }

        public IActionResult Stars(int movieId)
        {
            Movie movie=_movieRepository.GetMovieById(movieId, Convert.ToInt32(HttpContext.Session.GetString("userId")));
            Rating rating = new Rating();
            rating.IdTMDB = movie.idTMDB;
            rating.rate = Convert.ToInt32(movie.raiting);
            rating.type = MediType.Movie;

            return PartialView("~/Views/Stars.cshtml", rating);
        }
    }
}

