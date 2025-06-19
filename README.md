# ✅ ToDoApi – Minimal .NET Web API with SQLite

A simple To-Do backend built with **.NET 8**, **Entity Framework Core**, and **SQLite**.  
Includes full **xUnit test coverage** and clean DTO-based architecture.

---

## 🚀 Features

- 🧠 Minimal API structure
- 📦 SQLite local database
- ✨ AutoMapper for DTOs
- 🧪 Unit tests with xUnit
- 🔄 REST endpoints with filtering
- ✅ Entity validation

---

## 🗂️ Project Structure

ToDoApi/
│
├── Models/ # Domain models
├── DTOs/ # Data Transfer Objects
├── Services/ # Business logic (TodoService)
├── Data/ # DbContext + migrations
├── Migrations/ # EF migrations
├── Program.cs # Main API entry point
└── Tests/ # xUnit test project


---

## 📦 Run the API

```bash
dotnet run --project ToDoApi

Access at: https://localhost:7127/api/todo
🧪 Run tests

dotnet test Tests/Tests.csproj

📬 Sample Endpoints

    GET /api/todo – Get all todos

    GET /api/todo/{id} – Get todo by ID

    POST /api/todo – Create new todo

🛠️ Technologies Used

    .NET 8

    SQLite

    Entity Framework Core

    AutoMapper

    xUnit

    Swagger

📌 Author

Abilkhan Abdimazhitov
Intern-ready junior .NET backend developer
