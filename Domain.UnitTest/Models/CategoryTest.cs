using Domain.DTOs.Categories;
using Domain.Models;
using Xunit;

namespace Domain.UnitTest.Models
{
    public class CategoryTest
    {
        private const string DEFAULT_LABEL = "Default Category";

        [Fact]
        public void Update_ShouldUpdate_WhenPayloadIsValid()
        {
            //Arrange
            var category = CreateCategory();
            var payload = CreatePayload("Test Category");

            //Act
            category.Update(payload);

            //Assert
            Assert.True(
                category.Label == payload.Label);
        }

        [Fact]
        public void Update_ShouldNotUpdate_WhenPayloadIsNotValid()
        {
            //Arrange
            var category = CreateCategory();
            var payload = CreatePayload();

            //Act
            category.Update(payload);

            //Assert
            Assert.True(
                category.Label != payload.Label);
        }

        public static Category CreateCategory()
        {
            return new Category
            {
                Id = 1,
                Label = DEFAULT_LABEL
            };
        }

        public static CategoryValidator CreatePayload(
            string label = null)
        {
            return new CategoryValidator
            {
                Label = label
            };
        }
    }
}
