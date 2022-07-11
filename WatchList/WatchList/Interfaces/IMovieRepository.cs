using WatchList.Models;

namespace WatchList.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAll();
        Task<IEnumerable<Movie>> GetAllByUserId(int userId);
        List<Movie> GetMovieFromList(int userId);

        Movie GetMovieById(int movieId, int userId);
        bool Add(Movie movie);
        bool Update(Movie movie);
        bool Save();
    }
}
