namespace store_api.Services
{
    public interface IBaseService<TEntity, TKey> where TEntity : class
    {
        TEntity Find(TKey id);
        void Save(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TKey id);
    }
}