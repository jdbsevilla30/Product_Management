using Product_Management.Model;

namespace Product_Management.Interfaces.IServices
{
    public interface IProductManagementService
    {
        Task AddProduct();

        Task GetTotalValue();

        Task ListProducts();

        Task RemoveProduct();

        Task UpdateProduct();
    }
}