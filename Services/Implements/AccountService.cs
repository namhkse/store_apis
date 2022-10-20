using Microsoft.EntityFrameworkCore;
using store_api.Models;

namespace store_api.Services
{
    public class AccountService : BaseService<Account, int>, IAccountService
    {
        public AccountService(NorthwindContext context) : base(context)
        {
        }

        public override Account Find(int id)
        {
            return _context.Accounts.Include(a => a.Role).FirstOrDefault(a => a.AccountId == id);
        }

        public Account FindAccount(int id)
        {
            throw new NotImplementedException();
        }

        public Account FindAccount(string username, string password)
        {
            return _context.Accounts
                .Where(a => a.Username.Equals(username) && a.Password.Equals(password))
                .Include(a => a.Role)
                .FirstOrDefault();
        }

        public List<Account> GetAccounts()
        {
            throw new NotImplementedException();
        }
    }
}