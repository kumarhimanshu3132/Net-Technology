# Experiment 10: Student Management REST API

## AIM
Build a Student Management RESTful Web API using ASP.NET Core (.NET 8.0) demonstrating clean architecture, custom middleware pipeline integration (request logging and global exception handling), in-memory thread-safe repository pattern, and interactive API documentation with Swagger/OpenAPI.

## Description
This project implements a complete RESTful Web API for managing student records. It illustrates the core concepts of ASP.NET Core Web API development including separation of concerns (Models, Repositories, Middleware, and Controllers), dependency injection, RESTful HTTP action verbs (GET, POST, PUT, DELETE), response status codes (200, 201, 404, 500), and custom HTTP request pipeline processing using middleware. The data layer utilizes a thread-safe in-memory repository pre-seeded with 5 realistic student records, enabling immediate execution without external database configuration.

## Key Features
- **Clean Architecture & Folder Structure**: Distinct separation across `Models/`, `Repositories/`, `Middleware/`, and `Controllers/`.
- **In-Memory Thread-Safe Data Storage**: `InMemoryStudentRepository` backed by `ConcurrentDictionary` and `Interlocked` ID increment, pre-seeded with 5 realistic records.
- **Custom Request Logging Middleware**: Measures request execution time, writes HTTP method, path, and status code to the console, and injects the custom `X-Response-Time-ms` header into HTTP responses.
- **Global Exception Handling Middleware**: Centralized error management that catches unhandled server exceptions and returns structured JSON responses (`statusCode`, `message`, `details`).
- **Standard REST Endpoints**:
  - `GET /api/students`: Retrieve all students with optional `?branch={branch}` filtering.
  - `GET /api/students/{id}`: Retrieve a specific student by ID (returns 404 if not found).
  - `POST /api/students`: Create a new student record (returns 201 Created with `CreatedAtAction` location).
  - `PUT /api/students/{id}`: Update an existing student record (returns 200 OK or 404 if not found).
  - `DELETE /api/students/{id}`: Delete a student record (returns 200 OK or 404 if not found).
- **Interactive Swagger / OpenAPI**: Swagger UI pre-configured to open directly at `/swagger` on application launch for endpoint testing.

## Technologies Used
- C# (.NET 8.0)
- ASP.NET Core Web API
- Swashbuckle.AspNetCore (Swagger / OpenAPI UI)
- In-Memory Data Storage (`ConcurrentDictionary<int, Student>`)
