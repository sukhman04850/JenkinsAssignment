using Products_Microservice.DTO;

namespace Products_Microservice.Interfaces
{
    public interface IProductRepositoryBL
    {
        Task<List<ProductsDTO>> GetAllProducts();
        Task<ProductsDTO> GetProductById(Guid id);
        Task<ProductsDTO> AddProduct(ProductsDTO product);
        Task<ProductsDTO> UpdateProduct(ProductsDTO product);
        Task<bool> DeleteProduct(Guid id);
    }
}
