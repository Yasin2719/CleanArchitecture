using Domain.DTOs.Products;
using Domain.Interfaces;
using Domain.Utils;
using System;

namespace Domain.Models
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public void Update(ProductValidator payload)
        {
            if (payload.Label.IsValid()) Label = payload.Label;
            if (payload.Description.IsValid()) Description = payload.Description;
            if (payload.Price > 0) Price = payload.Price;

            UpdatedAt = DateTime.Now;
        }
    }
}
