using Microsoft.EntityFrameworkCore;
using WatchList.Data;
using WatchList.Interfaces;
using WatchList.Models;

namespace WatchList.Repository
{
    public class TVRepository : ITVRepository
    {
        private readonly DataContext _context;
        public TVRepository(DataContext context)
        {
            _context = context;
        }

        public bool Add(Series series)
        {
            _context.Add(series);
            return Save();
        }

        public async Task<IEnumerable<Series>> GetAll()
        {
            return await _context.Series_.ToListAsync();
        }

        public async Task<IEnumerable<Series>> GetAllByUserId(int userId)
        {
            return await _context.Series_.Where(i => i.idUser == userId).ToListAsync();
        }

        public Series GetSeriesById(int seriesId, int userId)
        {
            return _context.Series_.FirstOrDefault(i => i.idTMDB == seriesId && i.idUser == userId);
        }

        public List<Series> GetSeriesFromList(int userId)
        {
            return _context.Series_.Where(i => i.idUser == userId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Series series)
        {
            _context.Update(series);
            return Save();
        }
    }
}
