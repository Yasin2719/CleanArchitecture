using Domain.Abstractions;
using Domain.DTOs.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IProductService
    {
        Result<IEnumerable<ProductResponse>> GetProducts();
        Task<Result<ProductResponse>> CreateProduct(ProductValidator payload);
        Task<Result<ProductResponse>> GetProductById(int id);
        Task<Result<ProductResponse>> UpdateProduct(int id, ProductValidator payload);
        Task<Result<ProductResponse>> DeleteProduct(int id);
    }
}
