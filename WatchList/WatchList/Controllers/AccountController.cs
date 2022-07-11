using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using WatchList.Interfaces;
using WatchList.Models;
using WatchList.Models.TMDBParse;

namespace WatchList.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly ITVRepository _tVRepository;
        private ToTMDB toTMDB;
        public AccountController(IAccountRepository accountRepository, IMovieRepository movieRepository, ITVRepository tVRepository)
        {
            _accountRepository = accountRepository;
            _movieRepository = movieRepository;
            toTMDB = new ToTMDB();
            _tVRepository = tVRepository;
        }
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_accountRepository.CheckLogin(model.Login))
                {
                    User userModel = new User { email = model.Email, login = model.Login, password = EncryptionMD5.MD5Hash(model.Password) };
                    var result = _accountRepository.Add(userModel);
                    if (result)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

            }
            return View(model);
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(RegisterViewModel model)
        {
            if (model.Login != null && model.Password != null)
            {
                User user = await _accountRepository.GetByLogin(model.Login);

                if (user != null)
                {
                    if (user.password.Equals(EncryptionMD5.MD5Hash(model.Password)))
                    {
                        HttpContext.Session.SetString("userId", user.Id.ToString());

                        return RedirectToAction("ListPage", "Account");
                    }
                    else
                    {
                        ViewData["LogOrPass"] = "The Login or Password is incorrect.";

                        return View(model);
                    }
                }
                ViewData["LogOrPass"] = "The Login or Password is incorrect.";

            }


            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.Remove("userId");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ListPage()
        {
            List<Movie> moviesBuff = new List<Movie>();
            moviesBuff.AddRange(_movieRepository.GetMovieFromList(Convert.ToInt32(HttpContext.Session.GetString("userId"))));
            ViewData["ListMovieFromDB"] = moviesBuff;

            List<TMDB_Model> movies = new List<TMDB_Model>();
            foreach (var movie in moviesBuff)
            {
                TMDB_Model movie_ = toTMDB.GetMoreAboutMovieById(movie.idTMDB);
                movies.Add(movie_);
            }

            ViewData["ListMovie"] = movies;

            List<Series> tvBuff = new List<Series>();
            tvBuff.AddRange(_tVRepository.GetSeriesFromList(Convert.ToInt32(HttpContext.Session.GetString("userId"))));
            ViewData["ListTVFromDB"] = tvBuff;

            List<TMDB_Model> tv = new List<TMDB_Model>();
            foreach (var series in tvBuff)
            {
                TMDB_Model tv_ = toTMDB.GetMoreAboutTVById(series.idTMDB);

                List<SeasonsDetail> seasonsDetails = new List<SeasonsDetail>();
                seasonsDetails.AddRange(tv_.Seasons);
                seasonsDetails.Remove(seasonsDetails.FirstOrDefault(z => z.SeasonNumber == 0));
                tv_.Seasons = seasonsDetails.ToArray();
                tv.Add(tv_);
            }



            ViewData["ListTV"] = tv;

            return View();
        }


    }
}
