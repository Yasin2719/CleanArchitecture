using API;
using Application.Interfaces.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.IntegrationTest
{
    public class CategoryRepositoryTest : BasicIntegrationTest
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryRepositoryTest(
            WebApplicationFactory<Startup> factory) : base(factory)
        {
            _categoryRepository = _scope.ServiceProvider
                .GetRequiredService<ICategoryRepository>();
        }

        private const string LABEL = "Category Test";

        [Fact]
        public void GetCategories_ShouldReturnList()
        {
            //Arrange
            //Act
            var categories = _categoryRepository.GetCategories();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Category>>(categories);
        }

        [Fact]
        public async void Create_ShouldCreateCategory()
        {
            //Arrange
            var id = await GetUnassignedId();
            var category = new Category()
            {
                Id = id,
                Label = "Category Test"
            };

            //Act
            await _categoryRepository.CreateCategory(category);

            //Assert
            var createdCategory = await _dbContext.Categories.FindAsync(id);
            Assert.NotNull(createdCategory);

            await ClearCategories();
        }

        [Fact]
        public async void GetById_ShouldReturnCategory_WhenIdExist()
        {
            //Arrange
            var category = await CreateCategory();

            //Act
            var result = await _categoryRepository.GetCategoryById(category.Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(category.Label, result.Label);

            await ClearCategories();
        }

        [Fact]
        public async void Update_ShouldUpdate_WhenIdExist()
        {
            //Arrange
            var category = await CreateCategory();
            category.Label += " 2";

            //Act
            await _categoryRepository.UpdateCategory(category);

            //Assert
            var updatedCategory = await _dbContext.Categories
                                                    .FindAsync(category.Id);

            Assert.Equal(category.Label, updatedCategory.Label);
            Assert.NotEqual(LABEL, updatedCategory.Label);

            await ClearCategories();
        }

        [Fact]
        public async void Delete_ShouldDelete_WhenIdExist()
        {
            //Arrange
            var category = await CreateCategory();

            //Act
            await _categoryRepository.DeleteCategory(category);

            //Assert
            var updatedCategory = await _dbContext.Categories
                                                    .FindAsync(category.Id);

            Assert.Null(updatedCategory);

            await ClearCategories();
        }

        private async Task<int> GetUnassignedId()
        {
            var categories = await _dbContext.Categories
                                                .Select(c => c.Id)
                                                .ToListAsync();

            return (categories.Any() ? categories.Max() : 0) + 1;
        }

        private async Task<Category> CreateCategory()
        {
            var category = new Category
            {
                Label = LABEL,
                CreatedAt = DateTime.Now
            };

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        private async Task ClearCategories()
        {
            _dbContext.Categories.RemoveRange(
                await _dbContext.Categories.ToListAsync());
        }
    }
}
