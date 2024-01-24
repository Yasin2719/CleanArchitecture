using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Task<Category> CreateCategory(Category payload);
        Task<Category> GetCategoryById(int id);
        Task<Category> UpdateCategory(Category payload);
        Task DeleteCategory(Category category);
    }
}
