using Domain.DTOs.Products;
using AutoMapper;
using Domain.Models;

namespace Application.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductValidator, Product>();
            CreateMap<Product, ProductResponse>();
        }
    }
}
