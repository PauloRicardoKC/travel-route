using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelRoute.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Origin = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Destination = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Cost = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "Id", "Cost", "Destination", "Origin" },
                values: new object[,]
                {
                    { new Guid("1c70267a-b495-4fa5-bb46-e69676e564d6"), 10, "BRC", "GRU" },
                    { new Guid("2e229838-22f8-49e9-a5c8-39583f6dffe4"), 5, "SCL", "BRC" },
                    { new Guid("459fab17-8405-40cc-aeb8-1f5aacf08380"), 56, "ORL", "GRU" },
                    { new Guid("47e22155-f14a-4317-b9d5-0e0a6878fe1b"), 5, "CDG", "ORL" },
                    { new Guid("5b13d512-3818-4617-98a6-44689c9b8bd2"), 20, "ORL", "SCL" },
                    { new Guid("7a1f9b9f-d96d-4dea-807d-59da2015cffc"), 75, "CDG", "GRU" },
                    { new Guid("94a561ad-860a-4dc3-855b-05ccc0bb3b1e"), 20, "SCL", "GRU" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
