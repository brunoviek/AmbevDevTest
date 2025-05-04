# Ambev Developer Test

This repository contains the technical evaluation solution for the Ambev recruitment process. It is a .NET RESTful API containerized with Docker and orchestrated via Docker Compose.

---

## Overview

* Technology:

  * .NET 8 (C#)
  * MediatR, AutoMapper, FluentValidation
  * Docker and Docker Compose

* Key features:

  * CRUD operations for Products, Carts, Users, etc.
  * Dynamic pagination and sorting
  * Validation with FluentValidation
  * Integrated Swagger documentation

---

## Prerequisites

Before you begin, ensure you have installed:

1. [Docker](https://docs.docker.com/get-docker/)
2. [Docker Compose](https://docs.docker.com/compose/install/)
3. PowerShell 7+ (Windows) – recommended for running the `init.ps1` script

---

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/brunoviek/AmbevDevTest.git
   cd AmbevDevTest
   ```
2. (Optional) Build the solution:

   ```bash
   dotnet build Ambev.DeveloperEvaluation.sln
   ```

---

## Running the Application

### Using Docker Compose

```bash
docker-compose up --build -d
```

This will:

* Start the database container (configured in `docker-compose.yml`)
* Build and run the API container
* Run migrations and seed data (if configured in entrypoint)

### Recommended: Using `init.ps1` (Windows)

In PowerShell, run:

```powershell
.\init.ps1
```

The script will:

1. Build and start the containers
2. Wait for the database to respond (`pg_isready`, `sqlcmd`, or similar)
3. Run migrations and seed data if applicable

---

## API Access

* [Swagger UI](http://localhost:8080/swagger)
* [Health Check](http://localhost:8080/health)
* Main endpoints:

  * `GET http://localhost:8080/api/products`
  * `POST http://localhost:8080/api/carts`
  * `PUT http://localhost:8080/api/users/{id}`
  * ...

Adjust ports as needed in `docker-compose.yml` or `launchSettings.json`.

---

It is essential to have Docker and Docker Compose installed to run this solution. For Windows users, using `init.ps1` is recommended to automate the build and deployment process via containers.
