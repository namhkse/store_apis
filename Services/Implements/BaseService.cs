using Microsoft.EntityFrameworkCore;
using store_api.Models;

namespace store_api.Services
{
    public class BaseService<TEntity, TKey> : IBaseService<TEntity, TKey> where TEntity : class
    {
        protected readonly NorthwindContext _context;

        public BaseService(NorthwindContext context)
        {
            _context = context;
        }

        public virtual void Delete(TKey id)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public virtual TEntity Find(TKey id)
        {
            return _context.Find<TEntity>(id);
        }

        public virtual void Save(TEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}