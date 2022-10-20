using store_api.Models;

namespace store_api.Services
{
    public class TokenService : BaseService<Token, string>, ITokenService
    {
        // private readonly NorthwindContext _context;

        // public TokenService(NorthwindContext context)
        // {
        //     _context = context;
        // }

        // public void Delete(string id)
        // {
        //     throw new NotImplementedException();
        // }

        // public void Delete(Token entity)
        // {
        //     _context.Remove(entity);
        // }

        // public Token Find(string id)
        // {
        //     return _context.Tokens.Find(id);
        // }

        // public void Save(Token entity)
        // {
        //     _context.Tokens.Add(entity);
        //     _context.SaveChanges();
        // }

        // public void Update(Token entity)
        // {
        //     _context.Tokens.Update(entity);
        //     _context.SaveChanges();
        // }
        public TokenService(NorthwindContext context) : base(context)
        {
        }
    }
}