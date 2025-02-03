using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelRoute.Domain.Models;

namespace TravelRoute.Infra.Mappings
{
    public class RouteMapping : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .ValueGeneratedOnAdd();

            builder.Property(r => r.Origin)
                .IsRequired()
                .HasMaxLength(100); 

            builder.Property(r => r.Destination)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Cost)
                .IsRequired();

            builder.HasData(
                new Route("GRU", "BRC", 10) { Id = Guid.Parse("1c70267a-b495-4fa5-bb46-e69676e564d6") },
                new Route("BRC", "SCL", 5) { Id = Guid.Parse("2e229838-22f8-49e9-a5c8-39583f6dffe4") },
                new Route("GRU", "CDG", 75) { Id = Guid.Parse("7a1f9b9f-d96d-4dea-807d-59da2015cffc") },
                new Route("GRU", "SCL", 20) { Id = Guid.Parse("94a561ad-860a-4dc3-855b-05ccc0bb3b1e") },
                new Route("GRU", "ORL", 56) { Id = Guid.Parse("459fab17-8405-40cc-aeb8-1f5aacf08380") },
                new Route("ORL", "CDG", 5) { Id = Guid.Parse("47e22155-f14a-4317-b9d5-0e0a6878fe1b") },
                new Route("SCL", "ORL", 20) { Id = Guid.Parse("5b13d512-3818-4617-98a6-44689c9b8bd2") }
            );
        }
    }
}