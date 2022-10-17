using store_api.Model;

namespace store_api.Services {
    public class AccountService : IAccountService{
        public Account FindAccount(int id)
        {
            return SampleDatabase.Accounts.Find(a => a.Id == id);
        }

        public Account FindAccount(string username, string password)
        {
            return SampleDatabase.Accounts.Find(a => a.UserName.Equals(username) && a.Password.Equals(password));
        }

    }
}