using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Application.UnitTest.Fakers;
using Domain.DTOs.Products;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest
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
        public void GetProducts_ShouldReturnListOfProductResponse()
        {
            //Arrange
            //Act
            var result = _productService.GetProducts().ToList();

            Assert.IsType<List<ProductResponse>>(result);
        }

        [Fact]
        public async void CreateProdut_ShouldCreateProduct_WhenPayloadIsValid()
        {
            //Arrange
            var payload = CreateValidPayload();

            //Act
            var result = await _productService.CreateProduct(payload);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void CreateProduct_ShouldThrowException_WhenPayloadIsNotValid()
        {
            //Arrange
            var payload = CreateInValidPayload();

            //Act
            Task action() => _productService.CreateProduct(payload);

            //Assert
            await Assert.ThrowsAsync<ValidationException>(action);
        }

        [Fact]
        public async void GetProductById_ShouldReturnProduct_WhenProductExist()
        {
            //Arrange
            var product = await CreateProduct();

            //Act
            var response = await _productService
                .GetProductById(product.Id);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<ProductResponse>(response);
        }

        [Fact]
        public async void GetProductById_ShouldThrowAnException_WhenProductNotExist()
        {
            //Arrange
            const int productId = -1;

            //Act
            Task action() => _productService.GetProductById(productId);

            //Assert
            await Assert.ThrowsAsync<EntryPointNotFoundException>(action);
        }

        [Fact]
        public async void UpdateProduct_ShouldUpdateProduct_WhenProductExistAndPayloadIsValid()
        {
            //Arrange
            var product = await CreateProduct();
            var payload = CreateValidPayload();

            //Act
            var response = await _productService.UpdateProduct(product.Id, payload);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<ProductResponse>(response);
        }

        [Fact]
        public async void UpdateProduct_ShouldThrowException_WhenPayloadIsNotValid()
        {
            //Arrange
            var product = await CreateProduct();
            var payload = CreateInValidPayload();

            //Act
            Task action() => _productService.UpdateProduct(product.Id, payload);

            await Assert.ThrowsAsync<ValidationException>(action);
        }

        [Fact]
        public async void UpdateProduct_ShouldThrowException_WhenProductNotExist()
        {
            //Arrange
            const int productId = -1;
            var payload = CreateValidPayload();

            //Act
            Task action() => _productService.UpdateProduct(productId, payload);

            await Assert.ThrowsAsync<EntryPointNotFoundException>(action);
        }

        [Fact]
        public async void DeleteProduct_SouldDeleteProduct_WhenProductExist()
        {
            //Arrange
            var product = await CreateProduct();

            //Act
            await _productService.DeleteProduct(product.Id);

            //Assert
            var deletedProduct = await _productRepository.GetProductById(product.Id);
            Assert.Null(deletedProduct);
        }

        [Fact]
        public async void DeleteProduct_SouldThrowAnException_WhenProductNotExist()
        {
            const int productId = -1;

            Task action() => _productService.DeleteProduct(productId);

            await Assert.ThrowsAsync<EntryPointNotFoundException>(action);
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
