using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Product_Management.Interfaces.IServices;
using Product_Management.Interfaces.Repository.IReadOnly;
using Product_Management.Interfaces.Repository.IWriteOnly;
using Product_Management.Model;
using Product_Management.Repository.ReadOnlyRepository;
using Product_Management.Repository.WriteOnlyRepository;
using Product_Management.Services;

namespace Product_Management
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            var optionsBuilder = new DbContextOptionsBuilder<ProductManagementDbContext>();

            // Dependency Injectio
            serviceCollection.AddDbContext<ProductManagementDbContext>();
            serviceCollection.AddScoped<IProductReadOnlyRepository, ProductReadOnlyRepository>();
            serviceCollection.AddScoped<IProductWriteOnlyRepository, ProductWriteOnlyRepository>();
            serviceCollection.AddScoped<IProductManagementService, ProductManagementService>();


            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Valid option list. if this does not exist here, the application won't proceed.
            var validOptions = new List<string> { "1", "2", "3", "4", "5" };

            while (true)
            {
                Console.WriteLine("Select an operation:");
                Console.WriteLine("1: Add a product.");
                Console.WriteLine("2: Remove a product.");
                Console.WriteLine("3: Update a product.");
                Console.WriteLine("4: Retrieve the list of all products in the inventory.");
                Console.WriteLine("5: Calculate the total value of all products in the inventory.");
                Console.WriteLine("");
                Console.WriteLine("Please input your selection below:");
                Console.WriteLine("");
                string option = Console.ReadLine();

                // validation for option selected
                if (!validOptions.Contains(option))
                {
                    Console.WriteLine("Invalid option! Please select a valid option.");
                    continue;
                }

                // entry point for service 
                var productManagementService = serviceProvider.GetService<IProductManagementService>();

                if (option == "1")
                {
                    productManagementService.AddProduct();

                }
                else if (option == "2")
                {
                    productManagementService.RemoveProduct();
                }
                else if (option == "3")
                {
                    productManagementService.UpdateProduct();
                }
                else if (option == "4")
                {
                    productManagementService.ListProducts();
                }
                else if (option == "5")
                {
                    productManagementService.GetTotalValue();
                }
            }
        }
    }
}