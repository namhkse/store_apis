using store_api.Model;

namespace store_api.Services {
    public interface IProductService {
        public List<Product> GetProducts();
        public List<Product> GetProductsWithPaging(int page, int pageSize);
        public Product FindProduct(int id);
        public void UpdateProduct(Product p);
        public Product InsertProduct(Product p);
        public void DeleteProduct(int id);
    }
}