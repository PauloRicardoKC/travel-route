using TravelRoute.Application.Interfaces;
using TravelRoute.Domain.Interfaces;
using TravelRoute.Domain.Models;

namespace TravelRoute.Application.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;

        public RouteService(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task AddRouteAsync(string origin, string destination, int cost)
        {
            var existingRoutes = await _routeRepository.GetRoutesAsync();

            if (existingRoutes.Any(r => r.Origin == origin && r.Destination == destination))
            {
                throw new ArgumentException("A rota já existe.");
            }

            await _routeRepository.AddRouteAsync(new Route(origin, destination, cost));
        }

        public async Task<string> FindCheapestRouteAsync(string origin, string destination)
        {
            var routes = await _routeRepository.GetRoutesAsync();
            var paths = new List<(List<string> path, int cost)>();
            FindPaths(origin, destination, new List<string>(), 0, paths, routes);

            var cheapest = paths.OrderBy(p => p.cost).FirstOrDefault();
            return cheapest.path != null
                ? string.Join(" - ", cheapest.path) + $" ao custo de ${cheapest.cost}"
                : "Nenhuma rota encontrada";
        }

        private static void FindPaths(string current, string destination, List<string> path, int cost, List<(List<string>, int)> paths, List<Route> routes)
        {
            path.Add(current);

            if (current == destination)
            {
                paths.Add((new List<string>(path), cost));
                return;
            }

            foreach (var route in routes.Where(r => r.Origin == current))
            {
                if (!path.Contains(route.Destination)) 
                    FindPaths(route.Destination, destination, new List<string>(path), cost + route.Cost, paths, routes);
            }
        }
    }
}