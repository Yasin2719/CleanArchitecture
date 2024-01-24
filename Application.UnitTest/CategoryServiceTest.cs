using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Application.UnitTest.Fakers;
using Domain.DTOs.Categories;
using Domain.DTOs.Products;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest
{
    public class CategoryServiceTest
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;

        public CategoryServiceTest()
        {
            _categoryRepository = new FakeCategoryRepository();
            _categoryService = new CategoryService(
                _categoryRepository,
                FakeCategoryMapper.Create());
        }

        [Fact]
        public void GetCategories_ShouldListOfCategoryResponse()
        {
            //Arrange
            //Act
            var result = _categoryService.GetCategories();

            //Assert
            Assert.IsType<List<CategoryResponse>>(result);
        }

        [Fact]
        public async void CreateCategory_ShouldCreateCategory_WhenPayloadIsValid()
        {
            //Arrange
            var payload = CreateValidPayload();

            //Act
            var response = await _categoryService.CreateCategory(payload);

            //Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async void CreateCategory_ShouldThrowAnException_WhenPayloadIsNotValid()
        {
            //Arrange
            var payload = CreateInValidPayload();

            //Act
            Task action() => _categoryService.CreateCategory(payload);

            //Assert
            await Assert.ThrowsAsync<ValidationException>(action);
        }

        [Fact]
        public async void GetCategoryById_ShouldGetCategoryResponse_WhenCategoryExist()
        {
            //Arrange
            var category = await CreateCategory("Manually Created Category Test");

            //Act
            var response = await _categoryService
                .GetCategoryById(category.Id);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<CategoryResponse>(response);
        }

        [Fact]
        public async void GetCategoryById_ShouldThrowAnException_WhenCategoryNotExist()
        {
            //Arrange
            const int categoryId = -1;

            //Act
            Task action() => _categoryService.GetCategoryById(categoryId);

            //Assert
            await Assert.ThrowsAsync<EntryPointNotFoundException>(action);
        }

        [Fact]
        public async void UpdateCategory_ShouldUpdateCategory_WhenCategoryExist()
        {
            //Arrange
            var category = await CreateCategory("Manually Created Category Test");
            var payload = CreateValidPayload();

            //Act
            var response = await _categoryService.UpdateCategory(category.Id, payload);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<CategoryResponse>(response);
        }

        [Fact]
        public async void UpdateCategory_ShouldThrowAnException_WhenPayloadIsNotValid()
        {
            //Arrange
            var category = await CreateCategory("Manually Created Category Test");
            var payload = CreateInValidPayload();

            //Act
            Task action() => _categoryService.UpdateCategory(category.Id, payload);

            //Assert
            await Assert.ThrowsAsync<ValidationException>(action);
        }

        [Fact]
        public async void UpdateCategory_ShouldThrowAnException_WhenCategoryNotExist()
        {
            //Arrange
            const int categoryId = -1;
            var payload = CreateValidPayload();

            //Act
            Task action() => _categoryService.UpdateCategory(categoryId, payload);

            //Assert
            await Assert.ThrowsAsync<EntryPointNotFoundException>(action);
        }

        [Fact]
        public async void DeleteCategory_ShouldDeleteCategory_WhenCategoryExist()
        {
            //Arrange
            var category = await CreateCategory("Manually Created Category Test");

            //Act
            await _categoryService.DeleteCategory(category.Id);

            //Assert
            var deletedCategory = await _categoryRepository.GetCategoryById(category.Id);
            Assert.Null(deletedCategory);
        }

        [Fact]
        public async void DeleteCategory_ShouldThrowAnException_WhenCategoryNotExist()
        {
            //Arrange
            const int categoryId = -1;

            //Act
            Task action() => _categoryService.DeleteCategory(categoryId);

            //Assert
            await Assert.ThrowsAsync<EntryPointNotFoundException>(action);
        }



        private async Task<Category> CreateCategory(string label)
        {
            return await _categoryRepository
                .CreateCategory(new Category
                {
                    Label = label
                });
        }

        private static CategoryValidator CreateValidPayload()
        {
            return new CategoryValidator
            {
                Label = "Test Category"
            };
        }

        private static CategoryValidator CreateInValidPayload()
        {
            return new CategoryValidator
            {
                Label = null
            };
        }
    }
}
