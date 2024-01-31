using API;
using Application.Interfaces.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.IntegrationTest
{
    public class ProductRepositoryTest : BasicIntegrationTest
    {
        private readonly IProductRepository _productRepository;

        public ProductRepositoryTest(WebApplicationFactory<Startup> factory) : base(factory)
        {
            _productRepository = _scope.ServiceProvider
                .GetRequiredService<IProductRepository>();
        }

        [Fact]
        public async void Create_ShouldAddProduct_WhenPayloadIsValid()
        {
            //Arrange
            int id = await GetUnassignedId();
            var payload = new Product()
            {
                Id = id,
                Label = "laptop",
                Description = "new powerfull laptop",
                Price = 299.99m
            };

            //Act
            var product = await _productRepository.CreateProduct(payload);

            //Assert
            var createdProduct = _dbContext
                                    .Products
                                    .FirstOrDefault(p => p.Id == product.Id);
            Assert.NotNull(createdProduct);
            await ClearProducts();
        }

        [Fact]
        public async void GetById_ShouldReturnProduct_WhenProductExist()
        {
            //Arrange
            var createdProduct = await CreateProduct();

            //Act
            var product = await _productRepository.GetProductById(createdProduct.Id);

            //Assert
            Assert.NotNull(product);
            await ClearProducts();
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenProductNotExist()
        {
            //Arrange
            int id = await GetUnassignedId();

            //Act
            var product = await _productRepository.GetProductById(id);

            //Assert
            Assert.Null(product);
        }

        [Fact]
        public async void Update_ShouldUpdatePrice_WhenProductExist()
        {
            //Arrange
            var product = await CreateProduct();
            int id = product.Id;

            product.Price = 500;

            //Act
            var updatedProduct = await _productRepository.UpdateProduct(product);

            //Assert
            var createdProduct = _dbContext
                                    .Products
                                    .FirstOrDefault(p => p.Id == product.Id);

            Assert.Equal(product.Price, updatedProduct.Price);
            await ClearProducts();
        }

        [Fact]
        public async Task Delete_ShouldDeleteProduct_WhenProductExist()
        {
            //Arrange
            var product = await CreateProduct();
            int id = product.Id;

            //Act
            var exception = await Record.ExceptionAsync(() => _productRepository.DeleteProduct(id));

            //Assert
            var deletedProduct = _dbContext
                .Products
                .FirstOrDefault(p => p.Id == id);

            Assert.Null(exception);
            Assert.Null(deletedProduct);
        }

        private async Task<Product> CreateProduct()
        {
            int id = await GetUnassignedId();

            var product = await _dbContext.Products
                                            .AddAsync(new Product
                                            {
                                                Id = id,
                                                Label = "Samsung Tv",
                                                Description = "The last, powerfull TV of Samsung",
                                                Price = 1599.99m
                                            });
            await _dbContext.SaveChangesAsync();
            return product.Entity;
        }

        private async Task<int> GetUnassignedId()
        {
            var products = await _dbContext.Products
                                            .Select(c => c.Id)
                                            .ToListAsync();

            return (products.Any() ? products.Max() : 0) + 1;
        }

        private async Task ClearProducts()
        {
            _dbContext.Products.RemoveRange(
                await _dbContext.Products.ToListAsync());
        }
    }
}
