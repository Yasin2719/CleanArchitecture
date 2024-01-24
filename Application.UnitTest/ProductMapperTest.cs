using Domain.DTOs.Products;
using AutoMapper;
using Domain.Models;
using Xunit;
using Application.UnitTest.Fakers;

namespace Application.UnitTest
{
    public class ProductMapperTest
    {
        private readonly IMapper _mapper;

        public ProductMapperTest()
        {
            _mapper = FakeProductMapper.Create();
        }

        [Fact]
        public void Map_ShouldReturnProduct_ByProductValidator()
        {
            //Arrange
            var productDTO = new ProductValidator()
            {
                Label = "lorem ipsum",
                Description = "dolor sit amet",
                Price = 42.42m
            };

            //Act
            var product = _mapper.Map<Product>(productDTO);

            //Assert
            Assert.IsType<Product>(product);
        }

        [Fact]
        public void Map_ShouldReturnProductResponse_ByProduct()
        {
            //Arrange
            var product = new Product()
            {
                Id = 1,
                Label = "lorem ipsum",
                Description = "dolor sit amet",
                Price = 42.42m
            };

            //Act
            ProductResponse productResponse = _mapper.Map<ProductResponse>(product);

            //Assert
            Assert.IsType<ProductResponse>(productResponse);
        }

        [Fact]
        public void Map_ShouldThrowAnException_WhenMapProductValidatorToProductResponse()
        {
            //Arrange
            var productDTO = new ProductValidator()
            {
                Label = "lorem ipsum",
                Description = "dolor sit amet",
                Price = 42.42m
            };

            //Act
            //Assert
            Assert.ThrowsAny<AutoMapperConfigurationException>
                (() => _mapper.Map<ProductResponse>(productDTO));
        }
    }
}
