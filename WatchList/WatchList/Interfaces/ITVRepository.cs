using WatchList.Models;

namespace WatchList.Interfaces
{
    public interface ITVRepository
    {
        Task<IEnumerable<Series>> GetAll();
        Task<IEnumerable<Series>> GetAllByUserId(int userId);
        List<Series> GetSeriesFromList(int userId);

        Series GetSeriesById(int seriesId, int userId);
        bool Add(Series series);
        bool Update(Series series);
        bool Save();
    }
}
