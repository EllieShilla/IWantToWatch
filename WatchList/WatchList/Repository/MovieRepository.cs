using Microsoft.EntityFrameworkCore;
using WatchList.Data;
using WatchList.Interfaces;
using WatchList.Models;

namespace WatchList.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataContext _context;
        public MovieRepository(DataContext context)
        {
            _context = context;
        }
        public bool Add(Movie movie)
        {
            _context.Add(movie);
            return Save();
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllByUserId(int userId)
        {
            return await _context.Movies.Where(i => i.idUser == userId).ToListAsync();
        }

        public Movie GetMovieById(int movieId, int userId)
        {
            return _context.Movies.FirstOrDefault(i => i.idTMDB == movieId && i.idUser==userId);
        }

        public List<Movie> GetMovieFromList(int userId)
        {
            return _context.Movies.Where(i=>i.idUser==userId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Movie movie)
        {
            _context.Update(movie);
            return Save();
        }
    }
}
