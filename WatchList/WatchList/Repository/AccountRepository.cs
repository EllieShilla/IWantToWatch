using Microsoft.EntityFrameworkCore;
using WatchList.Data;
using WatchList.Interfaces;
using WatchList.Models;

namespace WatchList.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;
        public AccountRepository(DataContext context)
        {
            _context = context;
        }
        public bool Add(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool CheckLogin(string login)
        {
            User user= _context.Users.FirstOrDefault(i => i.login.Equals(login));
            if (user == null)
                return false;
            else
                return true;
        }

        public bool Delete(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(i=>i.Id==id);
        }

        public async Task<User> GetByLogin(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(i => i.login.Equals(login));
        }

        public async Task<User> GetByLoginAndPasswordAsync(string login, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(i => i.login.Equals(login) && i.password.Equals(password));
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(User user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
