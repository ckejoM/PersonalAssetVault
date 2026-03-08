# Personal Asset Vault 🏦

A modern, full-stack financial asset management dashboard. This project serves as the foundational pillar of a Senior Full-Stack Engineer portfolio, demonstrating strict adherence to Clean Architecture, robust API security, and highly optimized, reactive frontend state management.

## 🏗️ Architecture Overview

This solution strictly enforces separation of concerns, ensuring that the core business domain has zero dependencies on external frameworks or databases:
* **Client:** Angular 19 Client (Standalone / Signals)
* **API:** Presentation Layer (ASP.NET Core API)
* **Application:** Business Logic & CQRS (DTOs & Interfaces)
* **Domain:** Enterprise Entities & Rules
* **Infrastructure:** EF Core, Auth, & SQLite Database

## 💻 Tech Stack

**Backend (.NET 9):**
* **ASP.NET Core Web API:** RESTful endpoint design with centralized ProblemDetails exception handling.
* **Entity Framework Core (SQLite):** Code-first migrations with Fluent API configurations.
* **Mapster:** High-performance object mapping.
* **BCrypt & JWT:** Secure password hashing and stateless authorization via Bearer tokens.

**Frontend (Angular):**
* **Standalone Components:** Modern, module-less architecture.
* **Signals & RxJS:** Fine-grained reactive state management replacing Zone.js overhead.
* **Tailwind CSS:** Utility-first styling for responsive, custom dashboards.
* **Functional Interceptors & Guards:** Lean, functional approaches to HTTP pipeline manipulation and route security.

## 🚀 Getting Started

### Prerequisites
* .NET SDK 9.0+
* Node.js v20+
* Angular CLI

### 1. Database Setup
Navigate to the root directory and apply the Entity Framework migrations to create the SQLite database:
`dotnet ef database update --project Infrastructure/Infrastructure.csproj --startup-project API/API.csproj`

### 2. Run the Backend API
Navigate to the API folder and run the project:
`cd API`
`dotnet run`

*The API will start at https://localhost:7123. Scalar documentation is available at /scalar/v1 in development mode.*

### 3. Run the Frontend Client
Open a new terminal window, navigate to the client folder, install packages, and serve:
`cd client-app`
`npm install`
`ng serve`

*Navigate to http://localhost:4200 in your browser. You will be redirected to the login screen.*

## 📁 Repository Structure

* **`Domain/`**: Enterprise entities and repository contracts.
* **`Application/`**: Application services, DTOs, and mapping profiles.
* **`Infrastructure/`**: DbContext, Repositories, and JWT token providers.
* **`API/`**: Thin controllers, Dependency Injection orchestration, and CORS/Auth middleware.
* **`client-app/`**: Angular SPA featuring layout shells, reactive forms, and global HTTP interceptors.

---
*Created by Jovan Madzic | Software Engineer* - [LinkedIn](https://www.linkedin.com/in/jovan-madzic-12093b202/)