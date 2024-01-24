using Domain.DTOs.Products;
using Domain.Interfaces;
using System;

namespace Domain.Models
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public void Update(ProductValidator payload)
        {
            Label =
                (string.IsNullOrEmpty(payload.Label) ||
                string.IsNullOrWhiteSpace(payload.Label)) ?
                Label :
                payload.Label;

            Description =
                (string.IsNullOrEmpty(payload.Description) ||
                string.IsNullOrEmpty(payload.Description)) ?
                Description :
                payload.Description;

            Price =
                payload.Price > 0 ?
                payload.Price :
                Price;
        }
    }
}
