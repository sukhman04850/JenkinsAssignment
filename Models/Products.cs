using System.ComponentModel.DataAnnotations;

namespace Products_Microservice.Models
{
    public class Products
    {
        [Key]
        public Guid ProductId { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }

        public string? Details { get; set; }
    }
}
