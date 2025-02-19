using Microsoft.EntityFrameworkCore;
using Product_Management.Interfaces.Repository.IReadOnly;
using Product_Management.Model;

namespace Product_Management.Repository.ReadOnlyRepository
{
    public class ProductReadOnlyRepository : IProductReadOnlyRepository
    {
        private readonly ProductManagementDbContext _context;

        public ProductReadOnlyRepository(ProductManagementDbContext context)
        {
            _context = context;
        }

        // retrieve total value of the price
        public async Task<decimal> GetTotalValue()
        {
            decimal totalAmount = await _context.Products
                .SumAsync(p => p.Price * p.QuantityInStock);

            return totalAmount;
        }

        // retrieve the list of the products
        public async Task<List<Product>> ListProducts()
        {
            var productsList = await _context.Products.ToListAsync();
            return productsList;
        }
    }
}