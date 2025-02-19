using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Product_Management.Model
{
    public class ProductManagementDbContext : DbContext
    {
        public ProductManagementDbContext(DbContextOptions<ProductManagementDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=datastore.db");
        }

        // Table design and config
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // set primary key
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);

            // set decimal to have 18 and 2 decimal places
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}