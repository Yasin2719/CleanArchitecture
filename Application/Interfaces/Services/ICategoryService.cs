using Domain.DTOs.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryResponse> GetCategories();
        Task<CategoryResponse> CreateCategory(CategoryValidator payload);
        Task<CategoryResponse> GetCategoryById(int id);
        Task<CategoryResponse> UpdateCategory(int id, CategoryValidator payload);
        Task DeleteCategory(int id);
    }
}
