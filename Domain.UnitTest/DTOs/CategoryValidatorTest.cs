using Domain.DTOs.Categories;
using Xunit;


namespace Domain.UnitTest.DTOs
{
    public class CategoryValidatorTest
    {
        [Fact]
        public void Validate_ShouldBeTrue_WhenPropertiesAreValid()
        {
            //Arrange
            var validator = new CategoryValidator()
            {
                Label = "lorem",
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
            var validator = new CategoryValidator()
            {
                Label = " ",
            };

            //Act
            var result = validator.Validate();

            //Assert
            Assert.False(result);
        }
    }
}
