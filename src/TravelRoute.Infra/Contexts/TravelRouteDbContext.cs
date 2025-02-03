using Microsoft.EntityFrameworkCore;
using TravelRoute.Domain.Models;
using TravelRoute.Infra.Mappings;

namespace TravelRoute.Infra.Contexts
{
    public class TravelRouteDbContext : DbContext
    {
        public TravelRouteDbContext(DbContextOptions<TravelRouteDbContext> options) : base(options) { }
        public DbSet<Route> Routes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RouteMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}