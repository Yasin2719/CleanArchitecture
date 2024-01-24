using Domain.DTOs.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IProductService
    {
        IEnumerable<ProductResponse> GetProducts();
        Task<ProductResponse?> CreateProduct(ProductValidator payload);
        Task<ProductResponse?> GetProductById(int id);
        Task<ProductResponse?> UpdateProduct(int id, ProductValidator payload);
        Task DeleteProduct(int id);
    }
}
