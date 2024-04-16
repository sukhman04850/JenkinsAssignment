using Microsoft.AspNetCore.Mvc;
using Products_Microservice.DTO;
using Products_Microservice.Interfaces;

namespace Products_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepositoryBL _repository;
       
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductRepositoryBL repository, ILogger<ProductController> logger)
        {
            _repository = repository;
            _logger = logger;
            
        }
        [HttpPost("AddProducts")]
        public async Task<IActionResult> AddProduct(ProductsDTO product)
        {
            try
            {
                var newProduct = await _repository.AddProduct(product);
                if (newProduct == null)
                {
                    _logger.LogWarning("Failed to add product {@Product}", product);
                    return BadRequest();
                }
                _logger.LogInformation("Product added successfully. Product details: {@Product}", newProduct);
                return Ok(newProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GettAllProducts()
        {
            try
            {
                var allProducts = await _repository.GetAllProducts();
                if (allProducts == null)
                {
                    _logger.LogWarning("No products found. Returning NotFound.");
                    return NotFound("List is empty ");
                }
                _logger.LogInformation("Retrieved {ProductCount} products. {@Product}", allProducts.Count, allProducts);
                return Ok(allProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var product = await _repository.GetProductById(id);
                if (product != null)
                {
                    _logger.LogInformation("Retrieved product by ID {ProductId}. Product details: {@Product}", id, product);
                    return Ok(product);
                }
            }
            catch
            {
                var product = await _repository.GetProductById(id);
                if (product != null)
                {
                    _logger.LogInformation("Retrieved product by ID {ProductId}. Product details: {@Product}", id, product);
                    return Ok(product);
                }
            }


            _logger.LogWarning("No product found with ID {ProductId}.", id);
            return NotFound("Product with this ID doesn't exists");
        }
        [HttpPut("updateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductsDTO products)
        {
            if (products == null)
            {
                _logger.LogError("Retrieved Products is null {@Products}", products);
                return BadRequest("Product is empty");
            }
            var updateProduct = await _repository.UpdateProduct(products);
            if (updateProduct != null)
            {
                _logger.LogInformation("Product is updated {@updateProduct}", updateProduct);
                return Ok(updateProduct);
            }
            else
            {
                return NotFound("Product is null");
            }
        }
        [HttpDelete("deleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var response = await _repository.DeleteProduct(id);
            return Ok(response);
        }
    }
}
