# Travel Route
Choose a cheaper travel route regardless of the number of connections.

## Instructions ## 
A trip from GRU to CDG has the following routes:

- GRU - BRC - SCL - ORL - CDG at a cost of $40
- GRU - ORL - CDG at a cost of $61
- GRU - CDG at a cost of $75
- GRU - SCL - ORL - CDG at a cost of $45

The best price is for route 1. Despite having more connections, its final cost is lower.

The result of the query in the program should be: GRU - BRC - SCL - ORL - CDG at a cost of $40.

## Tech Stack ##
We use the following technologies in this project:

Backend:

- .NET 8.0: A free, cross-platform, open source developer platform for building many different types of applications.
- C#: A modern object-oriented programming language developed by Microsoft.
- Entity Framework Core CLI (dotnet tool install --global dotnet-ef).

Testing:
- xUnit: A free, open source, community-focused unit testing tool for the .NET Framework.

Databases:
- SQLite: is a lightweight, self-contained, serverless relational database management system

## Project Structure ##
The project should be structured as follows:

```
root
├── src/
    ├──  TravelRoute.API
    ├──  TravelRoute.Application
    ├──  TravelRoute.Domain
    ├──  TravelRoute.Infra
├── tests/
└── README.md
```

## Migrations ##
dotnet ef migrations add InitialCreate --project src/TravelRoute.Infra --startup-project src/TravelRoute.API --context TravelRouteDbContext
dotnet ef database update --project src/TravelRoute.Infra --startup-project src/TravelRoute.API --context TravelRouteDbContext

## Test the endpoints ##
Add a Route:
```
curl -X POST "http://localhost:5254/api/routes" -H "Content-Type: application/json" -d '{"origin":"GRU","destination":"BRC","cost":10}'
```

Find the cheapest route:
```
curl -X GET "http://localhost:5254/api/routes/find?origin=GRU&destination=ORL"
```

## Design Decisions ##
**Layered Architecture**

The application follows Clean Architecture, divided into API, Application, Domain, and Infra, ensuring separation of concerns:

- API: Exposes the endpoints via controllers.
- Application: Business rules and processing logic.
- Domain: Contains the business models and fundamental interfaces.
- Infra: Data persistence and database access.

**Persistence with SQLite**

SQLite for simplicity and ease of use. Entity Framework Core handles data persistence.

**Use of Async/Await**

All methods use async/await to improve scalability and avoid unnecessary blocking.

**Dependency Injection (IoC)**

The project uses Dependency Injection to decouple classes, making testing and maintenance easier.

**Unit Testing with xUnit**

Unit tests use xUnit and Moq to ensure code quality without dependence on a real database.
