using Microsoft.EntityFrameworkCore;
using TravelRoute.Domain.Interfaces;
using TravelRoute.Domain.Models;
using TravelRoute.Infra.Contexts;

namespace TravelRoute.Infra.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly TravelRouteDbContext _context;

        public RouteRepository(TravelRouteDbContext context)
        {
            _context = context;
        }

        public async Task AddRouteAsync(Route route)
        {
            await _context.Routes.AddAsync(route);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Route>> GetRoutesAsync()
        {
            return await _context.Routes.ToListAsync();
        }
    }
}