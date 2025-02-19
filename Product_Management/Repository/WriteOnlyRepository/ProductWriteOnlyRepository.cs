using Product_Management.Interfaces.Repository.IWriteOnly;
using Product_Management.Model;

namespace Product_Management.Repository.WriteOnlyRepository
{
    public class ProductWriteOnlyRepository : IProductWriteOnlyRepository
    {
        private readonly ProductManagementDbContext _context;

        public ProductWriteOnlyRepository(ProductManagementDbContext context)
        {
            _context = context;
        }

        // Add product to the Db
        public async Task<bool> AddProduct(Product product)
        {
            _context.Products.Add(product);
            return (await _context.SaveChangesAsync() > 0);
        }

        // remove product
        public async Task<bool> RemoveProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            return (await _context.SaveChangesAsync() > 0);
        }

        // update product
        public async Task<bool> UpdateProduct(int productId, int newQuantity)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return false;
            }

            product.QuantityInStock = newQuantity;
            _context.Products.Update(product);
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}