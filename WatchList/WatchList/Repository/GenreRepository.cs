using Microsoft.EntityFrameworkCore;
using WatchList.Data;
using WatchList.Interfaces;
using WatchList.Models;

namespace WatchList.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;
        public GenreRepository(DataContext context)
        {
            _context= context;
        }
        public async Task<Genre> GetByIdTMDBAsync(int idTMDB)
        {
            return await _context.Genres.FirstOrDefaultAsync(i => i.idTMDB == idTMDB);
        }
    }
}
