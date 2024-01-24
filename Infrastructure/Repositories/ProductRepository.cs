using Application.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CleanArchitectureDbContext dc;

        public ProductRepository(CleanArchitectureDbContext dataContext)
        {
            dc = dataContext;
        }

        public IEnumerable<Product> GetProducts()
        {
            return dc.Products
                .OrderByDescending(product => product.CreatedAt)
                .ToList();
        }

        public async Task<Product> CreateProduct(Product product)
        {
            product.CreatedAt = DateTime.Now;

            await dc.Products.AddAsync(product);
            await dc.SaveChangesAsync();

            return product;
        }

        public async Task<Product> GetProductById(int id)
        {
            return await dc.Products
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            product.UpdatedAt = DateTime.Now;

            await dc.SaveChangesAsync();

            return product;
        }

        public async Task DeleteProduct(int id)
        {
            var product = await dc.Products.FindAsync(id);
            if (product != null)
            {
                dc.Products.Remove(product);
                await dc.SaveChangesAsync();
            }
        }
    }
}
