using Application.UnitTest.Fakers;
using AutoMapper;
using Domain.DTOs.Categories;
using Domain.Models;
using System;
using Xunit;

namespace Application.UnitTest.Categories
{
    public class CategoryMapperTest
    {
        private readonly IMapper _mapper;

        public CategoryMapperTest()
        {
            _mapper = FakeCategoryMapper.Create();
        }

        [Fact]
        public void Map_ShouldReturnCategory_WhenPayloadIsCategoryValidator()
        {
            //Arrange
            var payload = new CategoryValidator()
            {
                Label = "Category Test"
            };

            //Act
            var category = _mapper.Map<Category>(payload);

            //Assert
            Assert.IsType<Category>(category);
        }

        [Fact]
        public void Map_ShouldReturnCategoryResponse_WhenPayloadIsCategory()
        {
            //Arrange
            var category = new Category()
            {
                Id = 1,
                Label = "Category Test"
            };

            //Act
            var response = _mapper.Map<CategoryResponse>(category);

            //Assert
            Assert.IsType<CategoryResponse>(response);
        }

        [Fact]
        public void Map_ShouldThrowAnException_WhenMapCategoryValidatorToCategory()
        {
            //Arrange
            var payload = new CategoryValidator()
            {
                Label = "Category Test"
            };

            //Act
            //Assert
            Assert.ThrowsAny<AutoMapperConfigurationException>
                (() => _mapper.Map<CategoryResponse>(payload));
        }
    }
}
