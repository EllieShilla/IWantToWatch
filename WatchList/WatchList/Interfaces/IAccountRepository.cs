using WatchList.Models;

namespace WatchList.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByLoginAndPasswordAsync(string login, string password);
        Task<User> GetByLogin(string login);
        bool Add(User user);
        bool Update(User user);
        bool Delete(User user);
        bool Save();

        bool CheckLogin(string login);
    }
}
