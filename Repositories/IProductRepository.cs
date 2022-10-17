using store_api.Model;

namespace store_api.Repository {
    public interface IProductRepository : IRepository<Product>{
        public Product Find(int id);
        public List<Product> FindAll();

    }
}