using store_api.Model;

namespace store_api.Services
{
    public interface IAccountService
    {
        public Account FindAccount(int id);
        public Account FindAccount(string username, string password);
    }
}