using TravelRoute.Domain.Models;

namespace TravelRoute.Domain.Interfaces
{
    public interface IRouteRepository
    {
        Task AddRouteAsync(Route route);
        Task<List<Route>> GetRoutesAsync();
    }
}