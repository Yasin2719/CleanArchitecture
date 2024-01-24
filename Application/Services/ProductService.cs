using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Domain.DTOs.Products;
using Domain.Models;
using System;
using System.Linq;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            productRepository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<ProductResponse> GetProducts()
        {
            return productRepository
                .GetProducts()
                .ToList()
                .Select(product => mapper.Map<ProductResponse>(product));
        }

        public async Task<ProductResponse> CreateProduct(ProductValidator payload)
        {
            if (!payload.Validate())
            {
                throw new ValidationException("invalid payload");
            }

            var product = mapper.Map<Product>(payload);

            return mapper.Map<ProductResponse>(
                await productRepository.CreateProduct(product));
        }

        public async Task<ProductResponse> GetProductById(int id)
        {
            var product = await productRepository.GetProductById(id);

            if (product is null)
            {
                throw new EntryPointNotFoundException("product not found");
            }

            return mapper.Map<ProductResponse>(product);
        }

        public async Task<ProductResponse> UpdateProduct(int id, ProductValidator payload)
        {
            if (!payload.ValidateForUpdate())
            {
                throw new ValidationException("invalid payload");
            }

            var product = await productRepository.GetProductById(id);
            if (product is null)
            {
                throw new EntryPointNotFoundException("product not found");
            }

            product.Update(payload);

            return mapper.Map<ProductResponse>(
                await productRepository.UpdateProduct(product));
        }

        public async Task DeleteProduct(int id)
        {
            if (await productRepository.GetProductById(id) is null)
            {
                throw new EntryPointNotFoundException("product not found");
            };

            await productRepository.DeleteProduct(id);
        }
    }
}
