using store_api.Models;

namespace store_api.Services
{
    public interface IAccountService : IBaseService<Account, int>
    {
        public Account FindAccount(int id);
        public Account FindAccount(string username, string password);
        public List<Account> GetAccounts();
    }
}