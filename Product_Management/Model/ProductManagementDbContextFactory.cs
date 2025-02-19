using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Product_Management.Model
{
    public class ProductManagementDbContextFactory : IDesignTimeDbContextFactory<ProductManagementDbContext>
    {
        public ProductManagementDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductManagementDbContext>();

            return new ProductManagementDbContext(optionsBuilder.Options);
        }
    }
}