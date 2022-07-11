using WatchList.Models;

namespace WatchList.Interfaces
{
    public interface IGenreRepository
    {
        Task<Genre> GetByIdTMDBAsync(int idTMDB);
    }
}
