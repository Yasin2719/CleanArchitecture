using Domain.Abstractions;
using Xunit;

namespace Domain.UnitTest.Abstractions
{
    public class ResultTest
    {
        private readonly Error TestError = new("Test.Error", "This error occured to test the Result class");

        [Fact]
        public void Success_ShouldBeSuccess_WhenIsSuccess()
        {
            //Arrange
            //Act
            var result = Result<object>.Success();

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public void Success_ShouldBeReturnRequestedData_WhenIsSuccess()
        {
            //Arrange
            const string data = "Valid Test !";
            //Act
            var result = Result<string>.Success(data);

            //Assert
            Assert.Equal(data, result.Data);
        }

        [Fact]
        public void Success_ShouldBeFailure_WhenIsFailure()
        {
            //Arrange
            //Act
            var result = Result<object>.Failure(TestError);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Success_ShouldBeReturnTestError_WhenIsFailure()
        {
            //Arrange
            //Act
            var result = Result<object>.Failure(TestError);

            //Assert
            Assert.Equal(TestError, result.Error);
        }
    }
}
