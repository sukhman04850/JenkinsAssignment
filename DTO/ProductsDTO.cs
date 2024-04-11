namespace Products_Microservice.DTO
{
    public class ProductsDTO
    {
        public Guid ProductId { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }

        public string? Details { get; set; }
    }
}
