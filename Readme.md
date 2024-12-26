# Developer Evaluation Project

## Overview

This project is a web API for managing products and carts, built using ASP.NET Core and following clean architecture principles. It implements CRUD operations for products and carts, utilizing the Mediator pattern with MediatR, AutoMapper for object mapping, and FluentValidation for request validation.

## Features

- Create, Read, Update, and Delete operations for Products
- Create, Read, Update, and Delete operations for Carts
- Pagination and filtering support for retrieving products and carts
- Validation of incoming requests
- Event publishing for certain operations (e.g., SaleCreated, SaleModified, SaleCancelled)

## Project Structure

The project is organized into several layers:

- **WebApi**: Contains controllers and API-specific models
- **Application**: Houses command and query handlers, along with DTOs
- **Domain**: Defines core business logic and entities
- **Infrastructure**: Implements data access and external service integrations

## Key Components

- **Controllers**: Handle HTTP requests and responses
- **Handlers**: Process commands and queries using the Mediator pattern
- **Validators**: Ensure the validity of incoming requests
- **Profiles**: Define AutoMapper mappings between different object types
- **Repositories**: Manage data access operations

## Getting Started

1. Clone the repository.
2. Ensure you have .NET 8 SDK installed.
3. Navigate to the project directory.
4. Run `dotnet restore` to restore dependencies.
5. Run `dotnet build` to build the project.
6. Run `dotnet run --project src/Ambev.DeveloperEvaluation.WebApi` to start the API.

## API Endpoints

### Products

- **POST /api/products**: Create a new product.
- **GET /api/products**: Retrieve a list of products.
- **GET /api/products/{id}**: Retrieve a specific product.
- **PUT /api/products/{id}**: Update an existing product.
- **DELETE /api/products/{id}**: Delete a product.

### Carts

- **POST /api/carts**: Create a new cart.
- **GET /api/carts**: Retrieve a list of carts.
- **GET /api/carts/{id}**: Retrieve a specific cart.
- **PUT /api/carts/{id}**: Update an existing cart.
- **DELETE /api/carts/{id}**: Delete a cart.

## Testing

The project includes unit tests for various components. To run the tests:

```bash
dotnet test
```

## Dependencies

- MediatR
- AutoMapper
- FluentValidation
- NSubstitute (for testing)
- xUnit (for testing)