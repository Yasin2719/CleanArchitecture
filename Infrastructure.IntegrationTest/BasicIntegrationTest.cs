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
        protected readonly IServiceScope _scope;
        protected readonly CleanArchitectureDbContext _dbContext;


        public BasicIntegrationTest(WebApplicationFactory<Startup> factory)
        {
            _scope = factory.Services.CreateScope();
            _dbContext = _scope.ServiceProvider.GetRequiredService<CleanArchitectureDbContext>();
        }
    }
}
