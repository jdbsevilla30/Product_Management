using Product_Management.Model;

namespace Product_Management.Interfaces.Repository.IReadOnly
{
    public interface IProductReadOnlyRepository
    {
        Task<decimal> GetTotalValue();

        Task<List<Product>> ListProducts();
    }
}