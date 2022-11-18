using store_api.Models;

namespace store_api.Services
{
    public interface IProductService
    {
        public List<Product> GetProducts();
        public List<Product> GetProductsWithPaging(int page, int pageSize);
        public Product FindProduct(int id);
        public void UpdateProduct(Product product);
        public void InsertProduct(Product product);
        public void DeleteProduct(int id);
        public List<Product> FindTopSoldProduct(int top);
    }
}