using Domain.DTOs.Products;
using Domain.Models;
using Xunit;

namespace Domain.UnitTest.Models
{
    public class ProductTest
    {
        private const string DEFAULT_LABEL = "lorem ipsum";
        private const string DEFAULT_DESCRIPTION = "dolor sit amet";
        private const decimal DEFAULT_PRICE = 25.99m;

        [Fact]
        public void Update_ShouldUpdate_WhenPayloadIsValid()
        {
            //Arrange
            var product = CreateProduct();

            var payload = CreatePayload("dolor sit amet", "lorem ipsum", 99.25m);

            //Act
            product.Update(payload);

            //Assert
            var result =
                product.Label == payload.Label &&
                product.Description == payload.Description &&
                product.Price == payload.Price;

            Assert.True(result);
        }

        [Fact]
        public void Update_ShouldJustUpdateLabel_WhenPayloadJustContainLabel()
        {
            //Arrange
            var product = CreateProduct();

            var payload = CreatePayload(
                label: "new laptop");

            //Act
            product.Update(payload);

            //Assert
            var result =
                product.Label == payload.Label &&
                product.Description == DEFAULT_DESCRIPTION &&
                product.Price == DEFAULT_PRICE;

            Assert.True(result);
        }

        [Fact]
        public void Update_ShouldJustUpdateDescription_WhenPayloadJustContainDescription()
        {
            //Arrange
            var product = CreateProduct();

            var payload = CreatePayload(
                description: "new nextgen powerfull laptop");

            //Act
            product.Update(payload);

            //Assert
            var result =
                product.Label == DEFAULT_LABEL &&
                product.Description == payload.Description &&
                product.Price == DEFAULT_PRICE;

            Assert.True(result);
        }

        [Fact]
        public void Update_ShouldJustUpdatePrice_WhenPayloadJustContainPrice()
        {
            //Arrange
            var product = CreateProduct();

            var payload = CreatePayload(
                price: 99.25m);

            //Act
            product.Update(payload);

            //Assert
            var result =
                product.Label == DEFAULT_LABEL &&
                product.Description == DEFAULT_DESCRIPTION &&
                product.Price == payload.Price;

            Assert.True(result);
        }

        [Fact]
        public void Update_ShouldNotUpdate_WhenPayloadIsNotValid()
        {
            //Arrange
            var product = CreateProduct();

            var payload = CreatePayload();

            //Act
            product.Update(payload);

            //Assert
            var result =
                product.Label == DEFAULT_LABEL &&
                product.Description == DEFAULT_DESCRIPTION &&
                product.Price == DEFAULT_PRICE;

            Assert.True(result);
        }

        private static Product CreateProduct()
        {
            return new Product
            {
                Id = 1,
                Label = DEFAULT_LABEL,
                Description = DEFAULT_DESCRIPTION,
                Price = DEFAULT_PRICE
            };
        }

        private static ProductValidator CreatePayload(
            string label = null,
            string description = null,
            decimal price = 0)
        {
            return new ProductValidator
            {
                Label = label,
                Description = description,
                Price = price
            };
        }

    }
}
