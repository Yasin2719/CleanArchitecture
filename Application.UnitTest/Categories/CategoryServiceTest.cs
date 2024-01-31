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

namespace Application.UnitTest.Categories
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
        public void GetCategories_ShouldBeSuccess()
        {
            //Arrange
            //Act
            var result = _categoryService.GetCategories();

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void CreateCategory_ShouldBeSuccess_WhenPayloadIsValid()
        {
            //Arrange
            var payload = CreateValidPayload();

            //Act
            var result = await _categoryService.CreateCategory(payload);

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void CreateCategory_ShouldBeFailure_WhenPayloadIsNotValid()
        {
            //Arrange
            var payload = CreateInValidPayload();

            //Act
            var result = await _categoryService.CreateCategory(payload);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async void GetCategoryById_ShouldBeSuccess_WhenCategoryExist()
        {
            //Arrange
            var category = await CreateCategory("Manually Created Category Test");

            //Act
            var result = await _categoryService
                .GetCategoryById(category.Id);

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void GetCategoryById_ShouldBeFailure_WhenCategoryNotExist()
        {
            //Arrange
            const int categoryId = -1;

            //Act
            var result = await _categoryService.GetCategoryById(categoryId);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async void UpdateCategory_ShouldBeSuccess_WhenCategoryExist()
        {
            //Arrange
            var category = await CreateCategory("Manually Created Category Test");
            var payload = CreateValidPayload();

            //Act
            var result = await _categoryService.UpdateCategory(category.Id, payload);

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void UpdateCategory_ShouldThrowAnException_WhenPayloadIsNotValid()
        {
            //Arrange
            var category = await CreateCategory("Manually Created Category Test");
            var payload = CreateInValidPayload();

            //Act
            var result = await _categoryService.UpdateCategory(category.Id, payload);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async void UpdateCategory_ShouldBeFailure_WhenCategoryNotExist()
        {
            //Arrange
            const int categoryId = -1;
            var payload = CreateValidPayload();

            //Act
            var result = await _categoryService.UpdateCategory(categoryId, payload);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async void DeleteCategory_ShouldBeSuccess_WhenCategoryExist()
        {
            //Arrange
            var category = await CreateCategory("Manually Created Category Test");

            //Act
            var result = await _categoryService.DeleteCategory(category.Id);

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async void DeleteCategory_ShouldBeFailure_WhenCategoryNotExist()
        {
            //Arrange
            const int categoryId = -1;

            //Act
            var result = await _categoryService.DeleteCategory(categoryId);

            //Assert
            Assert.True(result.IsFailure);
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
