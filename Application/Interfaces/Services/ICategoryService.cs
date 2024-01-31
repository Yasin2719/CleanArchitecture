using Domain.Abstractions;
using Domain.DTOs.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Result<IEnumerable<CategoryResponse>> GetCategories();
        Task<Result<CategoryResponse>> CreateCategory(CategoryValidator payload);
        Task<Result<CategoryResponse>> GetCategoryById(int id);
        Task<Result<CategoryResponse>> UpdateCategory(int id, CategoryValidator payload);
        Task<Result<CategoryResponse>> DeleteCategory(int id);
    }
}
