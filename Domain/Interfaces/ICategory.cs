using System;

namespace Domain.Interfaces
{
    public interface ICategory
    {
        int Id { get; set; }
        string Label { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
