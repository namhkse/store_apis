using store_api.Models;

namespace store_api.Services {
    public class AccountService : IAccountService
    {
        private readonly NorthwindContext _context;

        public AccountService(NorthwindContext context)
        {
           _context = context; 
        }

        public Account FindAccount(int id)
        {
            return _context.Accounts.Find(id);
        }

        public Account FindAccount(string username, string password)
        {
            return _context.Accounts.Where(a => a.Username.Equals(username) && a.Password.Equals(password)).FirstOrDefault();
        }

        public List<Account> GetAccounts()
        {
            return _context.Accounts.ToList();
        }
    }
}