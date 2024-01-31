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
using Domain.Abstractions;
using Domain.Errors;

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

        public Result<IEnumerable<CategoryResponse>> GetCategories()
        {
            return Result<IEnumerable<CategoryResponse>>
                .Success(_categoryRepository
                    .GetCategories()
                    .Select(category => _mapper.Map<CategoryResponse>(category)));
        }

        public async Task<Result<CategoryResponse>> CreateCategory(CategoryValidator payload)
        {
            var category = _mapper.Map<Category>(payload);

            if (!payload.Validate())
            {
                return Result<CategoryResponse>.Failure(
                    CategoryErrors.InvalidPayload);
            }

            category = await _categoryRepository.CreateCategory(category);

            return Result<CategoryResponse>.Success(
                _mapper.Map<CategoryResponse>(category));
        }

        public async Task<Result<CategoryResponse>> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);

            if (category is null)
            {
                return Result<CategoryResponse>.Failure(
                    CategoryErrors.NotFound);
            }

            return Result<CategoryResponse>.Success(
                _mapper.Map<CategoryResponse>(category));
        }

        public async Task<Result<CategoryResponse>> UpdateCategory(int id, CategoryValidator payload)
        {
            if (!payload.Validate())
            {
                return Result<CategoryResponse>.Failure(
                    CategoryErrors.InvalidPayload);
            }

            var category = await _categoryRepository.GetCategoryById(id);
            if (category is null)
            {
                return Result<CategoryResponse>.Failure(
                    CategoryErrors.NotFound);
            }

            category.Update(payload);

            category = await _categoryRepository.UpdateCategory(category);

            return Result<CategoryResponse>.Success(
                _mapper.Map<CategoryResponse>(category));
        }
        public async Task<Result<CategoryResponse>> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category is null)
            {
                return Result<CategoryResponse>.Failure(
                    CategoryErrors.NotFound);
            }

            await _categoryRepository.DeleteCategory(category);

            return Result<CategoryResponse>.Success();
        }
    }
}
