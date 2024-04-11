using Products_Microservice.Models;

namespace Products_Microservice.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Products>> GetAllProducts();
        Task<Products> GetProductById(Guid id);
        Task<Products> AddProduct(Products product);
        Task<Products> UpdateProduct(Products product);
        Task<bool> DeleteProduct(Guid id);
    }
}
