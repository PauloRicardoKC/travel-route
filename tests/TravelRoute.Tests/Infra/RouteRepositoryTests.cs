using Microsoft.EntityFrameworkCore;
using TravelRoute.Domain.Models;
using TravelRoute.Infra.Contexts;
using TravelRoute.Infra.Repositories;
using Xunit;

namespace TravelRoute.Tests.Infra
{
    public class RouteRepositoryTests : IDisposable
    {
        private readonly TravelRouteDbContext _dbContext;
        private readonly RouteRepository _repository;

        public RouteRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<TravelRouteDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            _dbContext = new TravelRouteDbContext(options);
            _repository = new RouteRepository(_dbContext);
        }

        [Fact]
        public async Task AddRouteAsync_ShouldAddRouteToDatabase()
        {
            // Arrange
            var route = new Route("GRU", "CDG", 50);

            // Act
            await _repository.AddRouteAsync(route);
            var routes = await _dbContext.Routes.ToListAsync();

            // Assert
            Assert.Single(routes);
            Assert.Equal("GRU", routes[0].Origin);
            Assert.Equal("CDG", routes[0].Destination);
            Assert.Equal(50, routes[0].Cost);
        }

        [Fact]
        public async Task GetRoutesAsync_ShouldReturnAllRoutes()
        {
            // Arrange
            var route1 = new Route("GRU", "CDG", 50);
            var route2 = new Route("BRC", "SCL", 30);

            _dbContext.Routes.AddRange(route1, route2);
            await _dbContext.SaveChangesAsync();

            // Act
            var routes = await _repository.GetRoutesAsync();

            // Assert
            Assert.Equal(2, routes.Count);
            Assert.Contains(routes, r => r.Origin == "GRU" && r.Destination == "CDG");
            Assert.Contains(routes, r => r.Origin == "BRC" && r.Destination == "SCL");
        }

        public void Dispose()
        {
            _dbContext.Dispose(); 
        }
    }
}