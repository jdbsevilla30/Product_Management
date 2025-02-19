using Product_Management.Interfaces.IServices;
using Product_Management.Interfaces.Repository.IReadOnly;
using Product_Management.Interfaces.Repository.IWriteOnly;
using Product_Management.Model;
using System.Runtime.CompilerServices;

namespace Product_Management.Services
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly IProductReadOnlyRepository _productReadOnlyRepository;
        private readonly IProductWriteOnlyRepository _productWriteOnlyRepository;

        public ProductManagementService(IProductWriteOnlyRepository productWriteOnlyRepository, IProductReadOnlyRepository productReadOnlyRepository)
        {
            _productWriteOnlyRepository = productWriteOnlyRepository;
            _productReadOnlyRepository = productReadOnlyRepository;
        }

        #region Remove Product Service

        public async Task RemoveProduct()
        {
            try
            {
                Console.WriteLine("");
                Console.WriteLine("PRODUCT REMOVE");
                Console.WriteLine("");
                int productId = RemoveProductById();

                ConfirmYesNoOperation();

                bool removeProductSucceeded = await _productWriteOnlyRepository.RemoveProduct(productId);

                if (removeProductSucceeded)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Product with the product id {productId} has been deleted");
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Product has been already deleted or does not exist.");
                    Console.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine($"There is an exception in the program on RemoveProduct(). Ex: {ex}");
                Console.WriteLine("");
            }
        }

        private static int RemoveProductById()
        {
            string productId;
            int parsedId;

            do
            {
                Console.WriteLine("");
                Console.WriteLine("Please enter the product Id of the product you wanted to remove.");
                Console.WriteLine("");
                productId = Console.ReadLine();

                if (string.IsNullOrEmpty(productId))
                {
                    Console.WriteLine("");
                    Console.WriteLine("Product Id cannot be blank. Please enter a valid product Id.");
                    Console.WriteLine("");
                }

                if (!int.TryParse(productId, out parsedId))
                {
                    Console.WriteLine("");
                    Console.WriteLine("Invalid input. Please enter a valid numeric Product Id.");
                    Console.WriteLine("");
                    continue;
                }

                break;
            } while (true);

            return Int32.Parse(productId);
        }

        #endregion Remove Product Service

        #region Add Product Service

        public async Task AddProduct()
        {
            try
            {
                var product = new Product();
                Console.WriteLine("");
                Console.WriteLine("PRODUCT ADD");
                Console.WriteLine("");

                product.Name = GetValidProductName();
                product.QuantityInStock = GetValidProductQuantity();
                product.Price = GetValidProductPrice();

                Console.WriteLine("");
                Console.WriteLine("Are you sure you want to add this product? (Y/N)");
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.QuantityInStock}");
                Console.WriteLine("");

                ConfirmYesNoOperation();

                bool addProductSucceeded = await _productWriteOnlyRepository.AddProduct(product);

                if (addProductSucceeded)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Product {product.Name} has been added. Its price is {product.Price} and its initial quantity is {product.QuantityInStock}");
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Add product failure. Here's the Product object: {product.Name}, {product.ProductId}, {product.QuantityInStock}, {product.Price}");
                    Console.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        #region Add Product Helper. These private methods make sure that the Console isn't escaped if invalid inputs are inputted.

        // ValidProductName helper
        private static string GetValidProductName()
        {
            string productName;
            do
            {
                Console.WriteLine("");
                Console.WriteLine("Please enter the product name.");
                Console.WriteLine("");
                productName = Console.ReadLine();

                if (string.IsNullOrEmpty(productName))
                {
                    Console.WriteLine("");
                    Console.WriteLine("Product name cannot be empty. Please enter a valid name.");
                    Console.WriteLine("");
                }
            } while (string.IsNullOrEmpty(productName)); // Repeat if invalid input

            return productName;
        }

        // product price helper
        private static decimal GetValidProductPrice()
        {
            decimal price;
            string input;
            do
            {
                Console.WriteLine("");
                Console.WriteLine("Please enter the product price.");
                Console.WriteLine("");
                input = Console.ReadLine();

                // Check if the input is a valid decimal
                if (!decimal.TryParse(input, out price) || price <= 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Invalid price! Please enter a valid positive decimal number.");
                    Console.WriteLine("");
                }
            } while (!decimal.TryParse(input, out price) || price <= 0);

            return price;
        }

        // quantity helper for Add and/or Update
        private static int GetValidProductQuantity()
        {
            int quantityInStock;
            string input;
            do
            {
                Console.WriteLine("");
                Console.WriteLine("Please enter the product quantity.");
                Console.WriteLine("");
                input = Console.ReadLine();

                // Check if the input is a valid integer
                if (!int.TryParse(input, out quantityInStock) || quantityInStock < 1)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Invalid quantity! Please enter a valid positive integer.");
                    Console.WriteLine("");
                }
            } while (!int.TryParse(input, out quantityInStock) || quantityInStock < 1); // Repeat if invalid input

            return quantityInStock;
        }

        #endregion Add Product Helper. These private methods make sure that the Console isn't escaped if invalid inputs are inputted.

        #endregion Add Product Service

        public async Task UpdateProduct()
        {
            try
            {
                Console.WriteLine("");
                Console.WriteLine("Product update.");
                Console.WriteLine("");

                int productId = UpdateByProductId();
                int productQuantity = GetValidProductQuantity();

                ConfirmYesNoOperation();

                bool updateProductSucceeded = await _productWriteOnlyRepository.UpdateProduct(productId, productQuantity);

                if (updateProductSucceeded)
                {
                    Console.WriteLine($"Product with the product id {productId}'s quantity has been changed to {productQuantity}");
                }
                else
                {
                    Console.WriteLine($"There is a problem updating the product or the product id doesn't exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine($"There is an exception in the program on UpdateProduct(). Ex: {ex}");
                Console.WriteLine("");
            }
        }

        private static int UpdateByProductId()
        {
            string productId;
            int parsedId;

            do
            {
                Console.WriteLine("");
                Console.WriteLine("Please enter the product Id of the product you wanted to update.");
                Console.WriteLine("");
                productId = Console.ReadLine();

                if (string.IsNullOrEmpty(productId))
                {
                    Console.WriteLine("");
                    Console.WriteLine("Product Id cannot be blank. Please enter a valid product Id.");
                    Console.WriteLine("");
                }

                if (!int.TryParse(productId, out parsedId))
                {
                    Console.WriteLine("");
                    Console.WriteLine("Invalid input. Please enter a valid numeric Product Id.");
                    Console.WriteLine("");
                    continue;
                }

                break;
            } while (true);

            return Int32.Parse(productId);
        }

        #region Products list service

        public async Task ListProducts()
        {
            // checks if the list from the database exist. if it doesn't print only the message, else, print all
            var productsList = await _productReadOnlyRepository.ListProducts();
            if (!productsList.Any())
            {
                Console.WriteLine("");
                Console.WriteLine("There are no products found.");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Here are the products List: ");
                Console.WriteLine("");

                // iterates the product list to print the product entity data
                foreach (var product in productsList)
                {
                    Console.WriteLine($"Product Id: {product.ProductId} Name: {product.Name}, Price: {product.Price}, Stock: {product.QuantityInStock}");
                }
                Console.WriteLine("");
            }
        }

        #endregion Products list service

        #region GetTotalValue service

        public async Task GetTotalValue()
        {
            var totalValueOfInventory = await _productReadOnlyRepository.GetTotalValue();
            Console.WriteLine("");
            Console.WriteLine($"Total value of inventory: {totalValueOfInventory}");
            Console.WriteLine("");
        }

        #endregion GetTotalValue service

        private void ConfirmYesNoOperation()
        {
            string selection;
            do
            {
                // make sure that everything is in lower case and there are no whitespaces
                selection = Console.ReadLine().Trim().ToLower();

                // Yes no confirmation
                if (selection.ToLower() == "n")
                {
                    Console.WriteLine("Product addition cancelled.");
                    return;
                }
                else if (selection.ToLower() != "y")
                {
                    Console.WriteLine("Invalid input. Please enter 'Y' to confirm or 'N' to cancel.");
                }
            } while (selection != "y");
        }
    }
}