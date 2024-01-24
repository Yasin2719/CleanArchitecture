namespace Domain.DTOs.Products
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
