using System;
using Domain.DTOs.Categories;
using Domain.Interfaces;
using Domain.Utils;

namespace Domain.Models
{
    public class Category : ICategory
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public void Update(CategoryValidator payload)
        {
            if (payload.Label.IsValid()) Label = payload.Label;
            UpdatedAt = DateTime.Now;
        }
    }
}
