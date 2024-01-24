using Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.UnitTest.Fakers
{
    public class FakeProductRepository : IProductRepository
    {
        private List<Product> products;

        public FakeProductRepository()
        {
            products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Label = "lorem ipsum",
                    Description = "Dolor sit amet",
                    Price = 15
                }
            };
        }

        public IEnumerable<Product> GetProducts()
        {
            return products;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            product.Id = products.Select(p => p.Id).Max() + 1;
            product.CreatedAt = System.DateTime.Now;
            products.Add(product);

            return await Task.Run(() => product);
        }

        public async Task<Product> GetProductById(int id)
        {
            return await Task
                .Run(() => products
                .Where((p) => p.Id == id)
                .FirstOrDefault());
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            product.UpdatedAt = System.DateTime.Now;

            var index = products.FindIndex((p) => p.Id == product.Id);
            products[index] = product;

            return await Task.Run(() => product);
        }

        public async Task DeleteProduct(int id)
        {
            var product = await Task
                .Run(() => products
                .Where((p) => p.Id == id)
                .FirstOrDefault());

            await Task.Run(() => products.Remove(product));
        }
    }
}
