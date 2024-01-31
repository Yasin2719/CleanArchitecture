using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTOs.Products;
using Domain.Models;
using System.Linq;
using Domain.Errors;
using Domain.Abstractions;

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

        public Result<IEnumerable<ProductResponse>> GetProducts()
        {
            return Result<IEnumerable<ProductResponse>>
                .Success(productRepository
                            .GetProducts()
                            .ToList()
                            .Select(product => mapper.Map<ProductResponse>(product)));
        }

        public async Task<Result<ProductResponse>> CreateProduct(ProductValidator payload)
        {
            if (!payload.Validate())
            {
                return Result<ProductResponse>.Failure(
                    ProductErrors.InvalidPayload);
            }

            var product = mapper.Map<Product>(payload);

            return Result<ProductResponse>
                .Success(
                    mapper.Map<ProductResponse>(
                    await productRepository.CreateProduct(product)));
        }

        public async Task<Result<ProductResponse>> GetProductById(int id)
        {
            var product = await productRepository.GetProductById(id);

            if (product is null)
            {
                return Result<ProductResponse>.Failure(
                    ProductErrors.NotFound);
            }

            return Result<ProductResponse>
                .Success(mapper.Map<ProductResponse>(product));
        }

        public async Task<Result<ProductResponse>> UpdateProduct(int id, ProductValidator payload)
        {
            if (!payload.ValidateForUpdate())
            {
                return Result<ProductResponse>.Failure(
                    ProductErrors.InvalidPayload);
            }

            var product = await productRepository.GetProductById(id);
            if (product is null)
            {
                return Result<ProductResponse>.Failure(
                    ProductErrors.NotFound);
            }

            product.Update(payload);

            return Result<ProductResponse>
                .Success(mapper.Map<ProductResponse>(
                    await productRepository.UpdateProduct(product)));
        }

        public async Task<Result<ProductResponse>> DeleteProduct(int id)
        {
            if (await productRepository.GetProductById(id) is null)
            {
                return Result<ProductResponse>
                    .Failure(ProductErrors.NotFound);
            };

            await productRepository.DeleteProduct(id);

            return Result<ProductResponse>.Success();
        }
    }
}