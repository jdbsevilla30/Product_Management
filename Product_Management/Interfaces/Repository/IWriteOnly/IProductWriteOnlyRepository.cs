using Product_Management.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management.Interfaces.Repository.IWriteOnly
{
    public interface IProductWriteOnlyRepository
    {
        Task<bool> AddProduct(Product product);

        Task<bool> RemoveProduct(int productId);

        Task<bool> UpdateProduct(int productId, int newQuantity);
    }
}