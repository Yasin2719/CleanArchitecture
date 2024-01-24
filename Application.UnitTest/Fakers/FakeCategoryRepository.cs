using Application.Interfaces.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UnitTest.Fakers
{
    public class FakeCategoryRepository : ICategoryRepository
    {
        private List<Category> categories;

        public FakeCategoryRepository()
        {
            categories = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Label = "Test Category"
                }
            };
        }

        public IEnumerable<Category> GetCategories()
        {
            return categories.ToList(); ;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            category.Id = categories.Select(c => c.Id).Max() + 1;
            category.CreatedAt = DateTime.Now;

            categories.Add(category);

            return await Task.Run(() => category);
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await Task
                .Run(() => categories.FirstOrDefault(c => c.Id == id));
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            category.UpdatedAt = DateTime.Now;

            var index = categories.FindIndex(c => c.Id == category.Id);
            categories[index] = category;

            return await Task.Run(() => category);
        }

        public async Task DeleteCategory(Category category)
        {
            await Task.Run(() => categories.Remove(category));
        }
    }
}
