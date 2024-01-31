using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Application.UnitTest.Fakers;
using Domain.DTOs.Products;
using Domain.Errors;
using Domain.Models;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.Products
{
    public class ProductServiceTest
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;

        public ProductServiceTest()
        {
            _productRepository = new FakeProductRepository();
            _productService = new ProductService(
                _productRepository,
                FakeProductMapper.Create());
        }

        [Fact]
        public void GetProducts_ShouldBeSucess()
        {
            //Arrange
            //Act
            var response = _productService.GetProducts();

            //Assert
            Assert.True(response.IsSucess);
        }

        [Fact]
        public async void CreateProdut_ShouldBeSuccess_WhenPayloadIsValid()
        {
            //Arrange
            var payload = CreateValidPayload();

            //Act
            var response = await _productService.CreateProduct(payload);

            //Assert
            Assert.True(response.IsSucess);
        }

        [Fact]
        public async void CreateProduct_ShouldBeFailure_WhenPayloadIsNotValid()
        {
            //Arrange
            var payload = CreateInValidPayload();

            //Act
            var response = await _productService.CreateProduct(payload);

            //Assert
            Assert.True(response.IsFailure);
            Assert.Equal(ProductErrors.InvalidPayload, response.Error);
        }

        [Fact]
        public async void GetProductById_ShouldBeSuccess_WhenProductExist()
        {
            //Arrange
            var product = await CreateProduct();

            //Act
            var response = await _productService
                .GetProductById(product.Id);

            //Assert
            Assert.True(response.IsSucess);
        }

        [Fact]
        public async void GetProductById_ShouldBeFailure_WhenProductNotExist()
        {
            //Arrange
            const int productId = -1;

            //Act
            var response = await _productService.GetProductById(productId);

            //Assert
            Assert.True(response.IsFailure);
            Assert.Equal(ProductErrors.NotFound, response.Error);
        }

        [Fact]
        public async void UpdateProduct_ShouldBeSuccess_WhenProductExistAndPayloadIsValid()
        {
            //Arrange
            var product = await CreateProduct();
            var payload = CreateValidPayload();

            //Act
            var response = await _productService.UpdateProduct(product.Id, payload);

            //Assert
            Assert.True(response.IsSucess);
        }

        [Fact]
        public async void UpdateProduct_ShouldBeFailure_WhenPayloadIsNotValid()
        {
            //Arrange
            var product = await CreateProduct();
            var payload = CreateInValidPayload();

            //Act
            var response = await _productService.UpdateProduct(product.Id, payload);

            //Assert
            Assert.True(response.IsFailure);
            Assert.Equal(ProductErrors.InvalidPayload, response.Error);
        }

        [Fact]
        public async void UpdateProduct_ShouldBeFailure_WhenProductNotExist()
        {
            //Arrange
            const int productId = -1;
            var payload = CreateValidPayload();

            //Act
            var response = await _productService.UpdateProduct(productId, payload);

            //Assert
            Assert.True(response.IsFailure);
            Assert.Equal(ProductErrors.NotFound, response.Error);
        }

        [Fact]
        public async void DeleteProduct_SouldBeSuccess_WhenProductExist()
        {
            //Arrange
            var product = await CreateProduct();

            //Act
            var response = await _productService.DeleteProduct(product.Id);

            //Assert
            Assert.True(response.IsSucess);
        }

        [Fact]
        public async void DeleteProduct_SouldBeFailure_WhenProductNotExist()
        {
            //Arrange
            const int productId = -1;

            //Act
            var response = await _productService.DeleteProduct(productId);

            //Assert
            Assert.True(response.IsFailure);
            Assert.Equal(ProductErrors.NotFound, response.Error);
        }

        private async Task<Product> CreateProduct()
        {
            return await _productRepository
                .CreateProduct(new Product
                {
                    Label = "lorem ipsum",
                    Description = "Dolor sit amet",
                    Price = 15
                });
        }

        private static ProductValidator CreateValidPayload()
        {
            return new ProductValidator()
            {
                Label = "new Laptop",
                Description = "new Modern Laptop",
                Price = 300
            };
        }

        private static ProductValidator CreateInValidPayload()
        {
            return new ProductValidator()
            {
                Label = " ",
                Description = "",
                Price = 0
            };
        }
    }
}
