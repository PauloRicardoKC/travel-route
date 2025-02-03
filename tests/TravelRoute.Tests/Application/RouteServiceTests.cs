using Moq;
using TravelRoute.Application.Services;
using TravelRoute.Domain.Interfaces;
using TravelRoute.Domain.Models;
using Xunit;

namespace TravelRoute.Tests.Application
{
    public class RouteServiceTests
    {
        private readonly Mock<IRouteRepository> _mockRouteRepository;
        private readonly RouteService _routeService;

        public RouteServiceTests()
        {
            _mockRouteRepository = new Mock<IRouteRepository>();
            _routeService = new RouteService(_mockRouteRepository.Object);
        }

        [Fact]
        public async Task AddRouteAsync_ShouldAddRoute_WhenRouteDoesNotExist()
        {
            // Arrange
            var origin = "GRU";
            var destination = "BRC";
            var cost = 10;
            var existingRoutes = new List<Route>();

            _mockRouteRepository.Setup(r => r.GetRoutesAsync()).ReturnsAsync(existingRoutes);
            _mockRouteRepository.Setup(r => r.AddRouteAsync(It.IsAny<Route>())).Returns(Task.CompletedTask);

            // Act
            await _routeService.AddRouteAsync(origin, destination, cost);

            // Assert
            _mockRouteRepository.Verify(r => r.AddRouteAsync(It.Is<Route>(route => route.Origin == origin && route.Destination == destination && route.Cost == cost)), Times.Once);
        }

        [Fact]
        public async Task AddRouteAsync_ShouldThrowException_WhenRouteExists()
        {
            // Arrange
            var origin = "GRU";
            var destination = "BRC";
            var cost = 10;
            var existingRoutes = new List<Route>
            {
                new Route("GRU", "BRC", 5)
            };

            _mockRouteRepository.Setup(r => r.GetRoutesAsync()).ReturnsAsync(existingRoutes);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _routeService.AddRouteAsync(origin, destination, cost));
            Assert.Equal("A rota já existe.", exception.Message);
        }

        [Fact]
        public async Task FindCheapestRouteAsync_ShouldReturnCheapestRoute()
        {
            // Arrange
            var origin = "GRU";
            var destination = "CDG";
            var routes = new List<Route>
            {
                new Route("GRU", "BRC", 10),
                new Route("BRC", "SCL", 5),
                new Route("GRU", "CDG", 75),
                new Route("GRU", "SCL", 20),
                new Route("GRU", "ORL", 56),
                new Route("ORL", "CDG", 5),
                new Route("SCL", "ORL", 20)
            };

            _mockRouteRepository.Setup(r => r.GetRoutesAsync()).ReturnsAsync(routes);

            // Act
            var result = await _routeService.FindCheapestRouteAsync(origin, destination);

            // Assert
            Assert.Equal("GRU - BRC - SCL - ORL - CDG ao custo de $40", result);
        }

        [Fact]
        public async Task FindCheapestRouteAsync_ShouldReturnNoRouteFound_WhenNoRouteExists()
        {
            // Arrange
            var origin = "GRU";
            var destination = "XYZ";
            var routes = new List<Route>
            {
                new Route("GRU", "BRC", 10)
            };

            _mockRouteRepository.Setup(r => r.GetRoutesAsync()).ReturnsAsync(routes);

            // Act
            var result = await _routeService.FindCheapestRouteAsync(origin, destination);

            // Assert
            Assert.Equal("Nenhuma rota encontrada", result);
        }
    }
}