using Domain.DTOs.Products;
using Xunit;

namespace Domain.UnitTest.DTOs
{
    public class ProductValidatorTest
    {
        [Fact]
        public void Validate_ShouldBeTrue_WhenPropertiesAreValid()
        {
            //Arrange
            var validator = new ProductValidator()
            {
                Label = "lorem",
                Description = "ipsum dolor sit amet",
                Price = 42.42m
            };

            //Act
            var result = validator.Validate();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_ShouldBeFalse_WhenPropertiesAreInvalid()
        {
            //Arrange
            var validator = new ProductValidator()
            {
                Label = "lorem",
            };

            //Act
            var result = validator.Validate();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateForUpdate_ShouldBeTrue_WhenOnePropertyAreIsValid()
        {
            //Arrange
            var validator = new ProductValidator()
            {
                Price = 42.42m
            };

            //Act
            var result = validator.ValidateForUpdate();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateForUpdate_ShouldBeFalse_WhenPropertiesAreInvalid()
        {
            //Arrange
            var validator = new ProductValidator()
            {
                Label = "",
                Description = null,
                Price = 0
            };

            //Act
            var result = validator.ValidateForUpdate();

            //Assert
            Assert.False(result);
        }
    }
}
