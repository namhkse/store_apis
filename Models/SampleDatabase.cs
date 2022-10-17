namespace store_api.Model
{
    public static class SampleDatabase
    {
        public static int _productId = 100;

        public static int GenerateProductId()
        {
            return ++_productId;
        }
        public static List<Product> Products = new List<Product>() {
            new Product() {Id = 1, Description = "Iphone", Price = 100},
            new Product() {Id = 2, Description = "Iphone", Price = 100},
            new Product() {Id = 3, Description = "Iphone", Price = 100},
            new Product() {Id = 4, Description = "Iphone", Price = 100},
            new Product() {Id = 5, Description = "Iphone", Price = 100},
            new Product() {Id = 6, Description = "Iphone", Price = 100},
            new Product() {Id = 7, Description = "Iphone", Price = 100},
            new Product() {Id = 8, Description = "Iphone", Price = 100},
            new Product() {Id = 9, Description = "Iphone", Price = 100},
            new Product() {Id = 10, Description = "Iphone", Price = 100},
            new Product() {Id = 11, Description = "Iphone", Price = 100},
            new Product() {Id = 12, Description = "Iphone", Price = 100},
            new Product() {Id = 13, Description = "Iphone", Price = 100}
        };
    }
}