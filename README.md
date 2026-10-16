# 📦 InventoryPro - Inventory & Order Management API

A clean and robust RESTful API for real-time inventory tracking and order processing built with **.NET 8**, **Entity Framework Core**, and **SQL Server**.

---

## 🚀 Key Features

- **Automated Stock Validation**: Real-time inventory check and atomic deductions upon placing orders.
- **RESTful Endpoints**: Full CRUD for Products and atomic Order placement pipeline.
- **On-Demand DB Provisioning**: Automated database and schema creation using EF Core `EnsureCreated()`.
- **Interactive Documentation**: Integrated Swagger UI for testing endpoints.
- **Clean Project Architecture**: Separation of concerns across Core Entities, EF DbContext, and Web API.

---

## 🛠 Tech Stack

- **Runtime**: .NET 8.0 SDK
- **Language**: C# 12
- **Data Access**: Entity Framework Core 8 (SQL Server Provider)
- **Database**: Microsoft SQL Server LocalDB
- **API Documentation**: Swashbuckle (Swagger UI)

---

## ⚡ Quickstart

### 1. Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server LocalDB (`(localdb)\MSSQLLocalDB`)

### 2. Setup & Run
Clone the repo and run the API:
```bash
git clone https://github.com/PaRsA9000/InventoryPro-Api.git
cd InventoryPro-Api/Api
dotnet run
```
Access Swagger UI at:
`http://localhost:5255/swagger`

---

## 📌 API Endpoints

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/Products` | List all inventory products |
| `POST` | `/api/Products` | Create a new product entry |
| `GET` | `/api/Orders` | Retrieve all placed orders |
| `POST` | `/api/Orders` | Place order & automatically deduct product stock |