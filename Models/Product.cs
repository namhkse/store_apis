using System.ComponentModel.DataAnnotations;

namespace store_api.Model {
    public class Product {
        public int Id { get; set; }
        public string Description {get; set;}

        [Range(0.0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}