using Newtonsoft.Json;
using Products_Microservice.Interfaces;
using Products_Microservice.Models;

namespace Products_Microservice.Repository
{
    public class ProductRepository : IProductRepository
    {
        private const string FilePath = "products.json";

        public async Task<Products> AddProduct(Products product)
        {
            List<Products> existingProducts = await ReadProductsFromFile();

            product.ProductId = Guid.NewGuid();
            existingProducts.Add(product);
            await WriteProductsToFile(existingProducts);
            return product;

        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            List<Products> existingProducts = await ReadProductsFromFile();

            var productToRemove = existingProducts.FirstOrDefault(x => x.ProductId == productId);

            if (productToRemove != null)
            {
                existingProducts.Remove(productToRemove);


                await WriteProductsToFile(existingProducts);
                return true;
            }
            return false;

        }

        public async Task<List<Products>> GetAllProducts()
        {
            return await ReadProductsFromFile();
        }
        public async Task<Products> GetProductById(Guid id)
        {
            List<Products> existingProducts = await ReadProductsFromFile();
            var product = existingProducts.Find(x => x.ProductId == id);
            if (product == null)
            {
                throw new Exception();
            }
            return product;
        }

        public async Task<Products> UpdateProduct(Products product)
        {
            List<Products> existingProducts = await ReadProductsFromFile();

            var existing = existingProducts.FirstOrDefault(x => x.ProductId == product.ProductId);

            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
                existing.Details = product.Details;


                await WriteProductsToFile(existingProducts);

                return existing;
            }
            else
            {

                throw new Exception("NotFound");
            }

        }
        private async Task<List<Products>> ReadProductsFromFile()
        {
            string jsonContent = await File.ReadAllTextAsync(FilePath);
            List<Products> products = JsonConvert.DeserializeObject<List<Products>>(jsonContent);
            return products ?? new List<Products>();
        }

        private async Task WriteProductsToFile(List<Products> products)
        {
            string jsonContent = JsonConvert.SerializeObject(products, Newtonsoft.Json.Formatting.Indented);
            await File.WriteAllTextAsync(FilePath, jsonContent);
        }
    }
}
