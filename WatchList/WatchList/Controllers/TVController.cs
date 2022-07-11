using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using WatchList.Interfaces;
using WatchList.Models;
using WatchList.Models.TMDBParse;

namespace WatchList.Controllers
{
    public class TVController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        private readonly ITVRepository _tVRepository;
        private ToTMDB toTMDB;
        public TVController(IGenreRepository genreRepository, ITVRepository tVRepository)
        {
            _genreRepository = genreRepository;
            toTMDB = new ToTMDB();
            _tVRepository = tVRepository;
        }

        [HttpGet]
        public async Task<ActionResult> TopTV(int NumPage = 1)
        {
            var tuple = toTMDB.CallTopTVAPI(NumPage);
            TMDB_Model[] tv = tuple.Item1;

            foreach (var series in tv)
            {
                foreach (var item in series.GenresId)
                {
                    Genre genre = await _genreRepository.GetByIdTMDBAsync(item);
                    series.GenresInText.Add(genre.genre_name);
                }
            }

            Pages pages = new Pages() { CurrentPage = NumPage, MaxPageTV = tuple.Item2 };

            ViewData["Data"] = tv;
            ViewData["NumPage"] = pages;

            return View("TopTV");
        }

        public async Task<ActionResult> MoreInfoAboutTV(int id)
        {
            TMDB_Model datalist = toTMDB.GetMoreAboutTVById(id);

            List<Series> seriesBuff = new List<Series>();
            seriesBuff.AddRange(_tVRepository.GetSeriesFromList(Convert.ToInt32(HttpContext.Session.GetString("userId"))));
            TMDB_Images tMDB_Images = new TMDB_Images();
            tMDB_Images.backdrops = toTMDB.GetImagesFromTV(id).backdrops;
            tMDB_Images.results = toTMDB.GetVideosFromTV(id).results;

            TMDB_Model[] tv = toTMDB.SimilarTV(id);

            foreach (var series in tv)
            {
                foreach (var item in series.GenresId)
                {
                    Genre genre = await _genreRepository.GetByIdTMDBAsync(item);
                    series.GenresInText.Add(genre.genre_name);
                }
            }



            ViewData["ListTVFromDB"] = seriesBuff;
            ViewData["Data"] = datalist;
            ViewData["ImagesAndVideo"] = tMDB_Images;
            ViewData["SimilarTV"] = tv;

            return View();
        }

        [HttpPost]
        public IActionResult AddTVToList(int id)
        {
            if (HttpContext.Session.GetString("userId") != null)
            {
                Series tv_toAdd = new Series { idTMDB = id, isWatched = false, raiting = 0, idUser = Convert.ToInt32(HttpContext.Session.GetString("userId")), currentSeason=1 };
                _tVRepository.Add(tv_toAdd);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }
        }

        public IActionResult ToWatched(int id, int episodeNum, int seasonNum)
        {
            Series series = _tVRepository.GetSeriesById(id, Convert.ToInt32(HttpContext.Session.GetString("userId")));
            TMDB_Model serialFormTMDB = toTMDB.GetMoreAboutTVById(series.idTMDB);

            if (episodeNum == serialFormTMDB.Seasons[seasonNum].EpisodeCount && seasonNum == serialFormTMDB.NumberOfSeasons)
            {
                series.lastWatchedSeries = episodeNum;
                series.currentSeason = seasonNum;
                series.isWatched = true;
            }
            else if (episodeNum == serialFormTMDB.Seasons[seasonNum].EpisodeCount && seasonNum < serialFormTMDB.NumberOfSeasons)
            {
                series.lastWatchedSeries = 0;
                series.currentSeason = seasonNum+1;
            }
            else
            {
                series.lastWatchedSeries = episodeNum;
                series.currentSeason = seasonNum;
            }

            _tVRepository.Update(series);
            return RedirectToAction("ListPage", "Account");
        }

        public IActionResult Rate(int tvId, int rate)
        {
            Series series = _tVRepository.GetSeriesById(tvId, Convert.ToInt32(HttpContext.Session.GetString("userId")));
            series.raiting = rate;
            _tVRepository.Update(series);
            return RedirectToAction("ListPage", "Account");
        }

        public IActionResult Stars(int tvId)
        {
            Series series = _tVRepository.GetSeriesById(tvId, Convert.ToInt32(HttpContext.Session.GetString("userId")));
            Rating rating = new Rating();
            rating.IdTMDB = series.idTMDB;
            rating.rate = Convert.ToInt32(series.raiting);
            rating.type = MediType.Series;
            return PartialView("~/Views/Stars.cshtml", rating);
        }
    }
}
