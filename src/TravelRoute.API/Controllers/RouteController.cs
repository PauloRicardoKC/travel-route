using Microsoft.AspNetCore.Mvc;
using TravelRoute.Application.Interfaces;
using Route = TravelRoute.Domain.Models.Route;

namespace TravelRoute.API.Controllers
{
    [ApiController]
    [Route("api/routes")]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRoute([FromBody] Route route)
        {
            if (route == null || string.IsNullOrEmpty(route.Origin) || string.IsNullOrEmpty(route.Destination))
            {
                return BadRequest("Origem e destino são obrigatórios.");
            }

            try
            {
                await _routeService.AddRouteAsync(route.Origin, route.Destination, route.Cost);

                return Ok("Rota adicionada com sucesso.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpGet("find")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FindCheapestRoute([FromQuery] string origin, [FromQuery] string destination)
        {
            if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination))
            {
                return BadRequest("Origem e destino são obrigatórios.");
            }

            var result = await _routeService.FindCheapestRouteAsync(origin, destination);

             return Ok(result);
        }
    }
}