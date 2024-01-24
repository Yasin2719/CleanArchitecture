using API;
using Application.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Infrastructure.IntegrationTest
{
    public abstract class BasicIntegrationTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly CleanArchitectureDbContext _dbContext;
        protected readonly IProductRepository _productRepository;

        public BasicIntegrationTest(WebApplicationFactory<Startup> factory)
        {
            var scope = factory.Services.CreateScope();

            _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
            _dbContext = scope.ServiceProvider.GetRequiredService<CleanArchitectureDbContext>();

        }
    }
}
