namespace TravelRoute.Domain.Models
{
    public class Route
    {
        public Route(string origin, string destination, int cost)
        {
            Origin = origin;
            Destination = destination;
            Cost = cost;
        }

        public Guid Id { get; set; }
        public string Origin { get; private set; }
        public string Destination { get; private set; }
        public int Cost { get; private set; }
    }
}