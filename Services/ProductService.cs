using store_api.Model;

namespace store_api.Services
{
    public class ProductService : IProductService
    {
        public void DeleteProduct(int id)
        {
            SampleDatabase.Products.RemoveAll(p => p.Id == id);
        }

        public Product FindProduct(int id)
        {
            return SampleDatabase.Products.Find(p => p.Id == id);
        }

        public List<Product> GetProducts()
        {
            return SampleDatabase.Products;
        }

        public List<Product> GetProductsWithPaging(int page = 1, int pageSize = 2)
        {
            page = (page <= 0) ? 1 : page;
            int left = (page - 1) * pageSize;
            int numberOfProduct = SampleDatabase.Products.Count;
            int totalPage = SampleDatabase.Products.Count / pageSize + 1;

            if (page > totalPage) return null;

            if ((left + pageSize) > numberOfProduct)
            {
                pageSize = numberOfProduct - left;
            }

            return SampleDatabase.Products.GetRange(left, pageSize);
        }

        public Product InsertProduct(Product p)
        {
            p.Id = SampleDatabase.GenerateProductId();
            SampleDatabase.Products.Add(p);
            return p;
        }

        public void UpdateProduct(Product product)
        {
            var p = SampleDatabase.Products.Find(p => p.Id == product.Id);
            if (p is null)
            {
                InsertProduct(product);
            }
            else
            {
                p.Description = product.Description;
                p.Price = product.Price;
            }
        }
    }
}