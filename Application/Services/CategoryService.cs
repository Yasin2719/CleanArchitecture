using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.DTOs.Categories;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public IEnumerable<CategoryResponse> GetCategories()
        {
            return _categoryRepository
                .GetCategories()
                .Select(category => _mapper.Map<CategoryResponse>(category))
                .ToList();
        }

        public async Task<CategoryResponse> CreateCategory(CategoryValidator payload)
        {
            var category = _mapper.Map<Category>(payload);

            if (!payload.Validate())
            {
                throw new ValidationException("invalid payload");
            }

            return _mapper.Map<CategoryResponse>(
                await _categoryRepository.CreateCategory(category));
        }

        public async Task<CategoryResponse> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);

            if (category is null)
            {
                throw new EntryPointNotFoundException("category not found");
            }

            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task<CategoryResponse> UpdateCategory(int id, CategoryValidator payload)
        {
            if (!payload.Validate())
            {
                throw new ValidationException("invalid payload");
            }

            var category = await _categoryRepository.GetCategoryById(id);
            if (category is null)
            {
                throw new EntryPointNotFoundException("category not found");
            }

            category.Label = payload.Label;

            return _mapper.Map<CategoryResponse>(
                await _categoryRepository.UpdateCategory(category));
        }
        public async Task DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category is null)
            {
                throw new EntryPointNotFoundException("category not found");
            }

            await _categoryRepository.DeleteCategory(category);
        }
    }
}
