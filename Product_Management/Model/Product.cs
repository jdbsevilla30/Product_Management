using System.ComponentModel.DataAnnotations;

namespace Product_Management.Model
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int QuantityInStock { get; set; }
    }
}