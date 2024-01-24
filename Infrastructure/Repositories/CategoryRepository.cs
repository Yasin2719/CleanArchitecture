using Application.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{

    class CategoryRepository : ICategoryRepository
    {
        private readonly CleanArchitectureDbContext dc;

        public CategoryRepository(CleanArchitectureDbContext dataContext)
        {
            dc = dataContext;
        }

        public IEnumerable<Category> GetCategories()
        {
            return dc.Categories.ToList();
        }

        public async Task<Category> CreateCategory(Category category)
        {
            category.CreatedAt = DateTime.Now;

            await dc.Categories.AddAsync(category);
            await dc.SaveChangesAsync();

            return category;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await dc.Categories.FindAsync(id);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            category.UpdatedAt = DateTime.Now;

            await dc.SaveChangesAsync();

            return category;
        }

        public async Task DeleteCategory(Category category)
        {
            dc.Categories.Remove(category);
            await dc.SaveChangesAsync();
        }
    }
}
