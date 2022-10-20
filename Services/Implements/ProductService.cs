using Microsoft.EntityFrameworkCore;
using store_api.Models;
using store_api.Utils;

namespace store_api.Services
{
    public class ProductService : IProductService
    {
        private readonly NorthwindContext _context;

        public ProductService(NorthwindContext context)
        {
            _context = context;
        }

        public void DeleteProduct(int id)
        {
            var p = new Product();
            p.ProductId = id;
            _context.Products.Remove(p);
            _context.SaveChanges();
        }

        public Product FindProduct(int id)
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefault(p => p.ProductId == id);
        }

        public List<Product> FindTopSoldProduct(int top)
        {
            var ids = _context.OrderDetails
                        .GroupBy(o => o.ProductId)
                        .Select(g => new
                        {
                            Id = g.Key,
                            Count = g.Count()
                        })
                        .OrderByDescending(e => e.Count)
                        .Select(a => a.Id)
                        .Take(top).ToList();

            var products = _context.Products.Where(p => ids.Contains(p.ProductId)).ToList();

            return products;
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public List<Product> GetProductsWithPaging(int page, int pageSize)
        {
            return Pagination<Product>.Paginate(_context.Products, page, pageSize).ToList();
        }

        public void InsertProduct(Product p)
        {
            p.ProductId = 0;
            _context.Products.Add(p);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
        }
    }
}