using Domain.DTOs.Categories;
using AutoMapper;
using Domain.Models;

namespace Application.Mappers
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<CategoryValidator, Category>();
            CreateMap<Category, CategoryResponse>();
        }
    }
}
