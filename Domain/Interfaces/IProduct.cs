using System;

namespace Domain.Interfaces
{
    public interface IProduct
    {
        int Id { get; set; }
        string Label { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
