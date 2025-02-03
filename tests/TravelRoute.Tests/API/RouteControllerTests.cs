using Microsoft.AspNetCore.Mvc;
using Moq;
using TravelRoute.API.Controllers;
using TravelRoute.Application.Interfaces;
using TravelRoute.Domain.Models;
using Xunit;

namespace TravelRoute.Tests.API
{
    public class RouteControllerTests
    {
        private readonly Mock<IRouteService> _mockRouteService;
        private readonly RouteController _controller;

        public RouteControllerTests()
        {
            _mockRouteService = new Mock<IRouteService>();
            _controller = new RouteController(_mockRouteService.Object);
        }

        [Fact]
        public async Task AddRoute_ShouldReturnOk_WhenRouteIsAdded()
        {
            // Arrange
            var route = new Route("GRU", "BRC", 10);

            _mockRouteService.Setup(s => s.AddRouteAsync(route.Origin, route.Destination, route.Cost)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddRoute(route);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Rota adicionada com sucesso.", okResult.Value);
        }

        [Fact]
        public async Task AddRoute_ShouldReturnBadRequest_WhenRouteExists()
        {
            // Arrange
            var route = new Route("GRU", "BRC", 10);
            _mockRouteService.Setup(s => s.AddRouteAsync(route.Origin, route.Destination, route.Cost)).Throws(new ArgumentException("A rota já existe."));

            // Act
            var result = await _controller.AddRoute(route);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("A rota já existe.", badRequestResult.Value);
        }

        [Fact]
        public async Task AddRoute_ShouldReturnBadRequest_WhenOriginOrDestinationIsEmpty()
        {
            // Arrange
            var route = new Route("GRU", "", 10);
            _mockRouteService.Setup(s => s.AddRouteAsync(route.Origin, route.Destination, route.Cost)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddRoute(route);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Origem e destino são obrigatórios.", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task FindCheapestRoute_ShouldReturnOk_WhenRouteFound()
        {
            // Arrange
            var origin = "GRU";
            var destination = "CDG";
            var cheapestRoute = "GRU - BRC - SCL - ORL - CDG ao custo de $40";

            _mockRouteService.Setup(s => s.FindCheapestRouteAsync(origin, destination)).ReturnsAsync(cheapestRoute);

            // Act
            var result = await _controller.FindCheapestRoute(origin, destination);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(cheapestRoute, okResult.Value);
        }

        [Fact]
        public async Task FindCheapestRoute_ShouldReturnBadRequest_WhenOriginOrDestinationIsEmpty()
        {
            // Arrange
            var origin = "";
            var destination = "CDG";

            // Act
            var result = await _controller.FindCheapestRoute(origin, destination);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Origem e destino são obrigatórios.", badRequestResult.Value.ToString());
        }
    }
}