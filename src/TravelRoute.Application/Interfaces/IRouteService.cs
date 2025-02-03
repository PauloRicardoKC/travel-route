namespace TravelRoute.Application.Interfaces
{
    public interface IRouteService
    {
        Task AddRouteAsync(string origin, string destination, int cost);
        Task<string> FindCheapestRouteAsync(string origin, string destination);
    }
}