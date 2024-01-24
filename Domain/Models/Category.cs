using System;
using Domain.DTOs.Categories;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Category : ICategory
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public void Update(CategoryValidator payload)
        {
            Label = (string.IsNullOrEmpty(payload.Label) ||
                    string.IsNullOrWhiteSpace(payload.Label)) ?
                    Label : payload.Label;
        }
    }
}
