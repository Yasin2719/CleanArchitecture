using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Task<Product> CreateProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }
}
