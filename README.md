# Products Management API (N-Tier Architecture with Filters)

A professional, scalable ASP.NET Core Web API demonstrating advanced middleware implementation, N-Tier architecture principles, and robust error handling strategies.

The project showcases how to build a production-ready API using **Action Filters** for cross-cutting concerns (Logging, Auth, Exceptions) instead of cluttering business logic, along with a powerful **Generic Repository** pattern for data access.

## 🚀 Technologies Used

* **.NET 10 (Preview)**
* **ASP.NET Core Web API**
* **Entity Framework Core** (SQL Server)
* **C# 12/13**
* **Layered Architecture**: API → BLL (Services) → DAL (Repositories)
* **Asynchronous Programming**: `async/await` patterns
* **Dependency Injection**: Built-in IoC Container

## 📋 Features Overview

This API provides complete lifecycle management for **Products** with support for "Soft Delete", complex filtering, and a dedicated pipeline for security and monitoring.

### ⭐ Key Features

#### 1. Advanced Filtering Pipeline (Middleware Replacement)

Instead of standard middleware, this project demonstrates the power of **ASP.NET Core Filters**:

* **⏱️ LogFilter (`IActionFilter`):** Precision timing for every request. It uses `Stopwatch` to measure execution time and logs method/path details via `ILogger`, ensuring performance monitoring without touching controller code.
* **🛡️ AuthFilter (`IAuthorizationFilter`):** Secures the API by inspecting the custom header `X-API-KEY`. It short-circuits the pipeline with `401 Unauthorized` if the key is missing or invalid, protecting resources before they are even accessed.
* **⚠️ ExceptionFilter (`IExceptionFilter`):** A centralized error handling mechanism. It catches unhandled exceptions and transforms them into standardized JSON responses (e.g., `KeyNotFoundException` → `404 Not Found`), keeping the API clean and consistent.

*Registered globally in `Program.cs`:*

```csharp
options.Filters.Add<LogFilter>();       // Logging & Performance
options.Filters.Add<AuthFilter>();      // Security & Access Control
options.Filters.Add<ExceptionFilter>(); // Global Error Handling

```

#### 2. Robust Data Access (DAL)

* **Generic Repository Pattern:** A `BaseRepository<T>` handles common CRUD operations, reducing code duplication.
* **Soft Delete:** Implemented via **Global Query Filters**. Deleted products remain in the DB (`DeletedAt` is set) but are automatically hidden from all queries unless explicitly requested via `IgnoreQueryFilters()`.
* **Dynamic Search:** The `GetAll` method supports:
* Filtering by Name, Price Range, Date, State.
* Sorting by multiple fields.
* Pagination (PageNumber / PageSize).



#### 3. Clean Business Logic (BLL)

* **DTO Mapping:** Separation of Domain Entities (`Product`) from API Contracts (`CreateProductRequest`, `ProductResponse`).
* **Partial Updates:** The `Update` logic intelligently handles `null` values, updating only the fields that were actually sent by the client.

## 🧩 Project Structure

The solution follows strict **N-Tier Architecture**, separating concerns into three distinct projects:

```text
Home_6.sln
│
├── Home_6.API                      # Presentation Layer (Entry Point)
│   ├── Controllers/                # API Endpoints (Products)
│   ├── Filters/                    # Custom Filters (Log, Auth, Exception)
│   ├── Interfaces/                 # Service Contracts
│   ├── Requests/                   # Input DTOs (Create/Update models)
│   ├── Responses/                  # Output DTOs (Standardized JSON)
│   ├── Services/                   # Business Logic Implementations
│   └── Program.cs                  # App Configuration & DI
│
├── Home_6.BLL                      # Business Logic Layer (Core Domain)
│   ├── Enums/                      # Enumerations (ProductStateEnum)
│   ├── Properties/                 # Search & Filter Models
│   ├── Interfaces/                 # Repository Contracts
│   ├── Models/                     # Domain Entities (Product, EntityBase)
│   └── Wrappers/                   # Paged Response Wrapper
│
└── Home_6.DAL                      # Data Access Layer (Infrastructure)
    ├── Configurations/             # EF Core Fluent API & Query Filters
    ├── Migrations/                 # Database Schema History
    ├── Repositories/               # Data Access Logic (Generic + Specific)
    └── HomeContext.cs              # Entity Framework DbContext

```

## 🛠 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/your-username/Home_6.git

```

### 2. Navigate to the project

```bash
cd Home_6

```

### 3. Database Setup

Ensure you have SQL Server (or LocalDB) running. The connection string is in `Home_6.API/appsettings.json`.

**Apply Migrations:**

```bash
dotnet ef database update --project Home_6.DAL --startup-project Home_6.API --context HomeContext

```

## ▶️ Running the Application

Run the API from the terminal:

```bash
dotnet run --project Home_6.API

```

The application will start, typically on `http://localhost:5000` or `https://localhost:7001`.

## 🧪 Testing & Security

### Swagger UI

The project includes Swagger for API documentation.

* Go to: `https://localhost:7001/swagger` (check console for exact port).

### 🛡 Security (API Key)

The `AuthFilter` protects all endpoints. You **MUST** add the following header to your requests:

```text
X-API-KEY: SecretKey123

```

*(Note: The key is configured in `AuthFilter.cs` for educational purposes).*

Without this header, you will receive a `401 Unauthorized` response.

## 📁 Endpoints Overview

### 📦 Products

Base path: `/v1/products`

| Method | Route | Description |
| --- | --- | --- |
| `GET` | `/` | Get all products (Supports pagination, search, sorting) |
| `GET` | `/{id}` | Get product by ID |
| `POST` | `/` | Create a new product |
| `PUT` | `/{id}` | Update product details (Partial update supported) |
| `DELETE` | `/{id}` | Soft delete a product |
| `PATCH` | `/{id}/state` | Change product state (e.g., InStock, Sold) |

## 🎓 Educational Value

This project demonstrates:

* **Aspect-Oriented Programming (AOP)**: Using Filters to cleanly separate cross-cutting concerns from business logic.
* **Global Query Filters**: How to implement "Soft Delete" correctly in EF Core.
* **Centralized Error Handling**: Moving `try-catch` blocks out of controllers into a dedicated `ExceptionFilter`.
* **Professional Logging**: Using `ILogger` and `Stopwatch` for accurate performance metrics.

## 📜 License

This project is intended for educational use and portfolio demonstration.
