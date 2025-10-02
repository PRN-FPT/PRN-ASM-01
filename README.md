# ASM_01 - EV Retail Management System

## 📋 Project Overview

**ASM_01** is a comprehensive Electric Vehicle (EV) retail management system built with ASP.NET Core. This project provides a complete solution for managing EV inventory, dealer networks, distribution requests, and customer authentication in an electric vehicle retail environment.

## 🏗️ Architecture

The project follows a **3 layers architecture** with clear separation of concerns:

```
ASM_01.WebApp          (Presentation Layer)
    ├── Controllers
    ├── Views
    └── Models

ASM_01.BusinessLayer   (Business Logic Layer)
    ├── Services
    ├── DTOs
    └── Abstract Interfaces

ASM_01.DataAccessLayer (Data Access Layer)
    ├── Entities
    ├── Repositories
    ├── Migrations
    └── Persistence
```

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 9.0 (MVC)
- **Language**: C# 11.0
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework Core 9.0
- **Authentication**: Cookie-based Authentication
- **Frontend**: Razor Views with Bootstrap styling
- **Architecture**: Repository Pattern with Unit of Work
- **Dependency Injection**: Built-in ASP.NET Core DI Container

## ✨ Key Features

### 🔐 Authentication System
- **Role-based Access Control**: Two distinct user roles
  - **DEALER**: Can manage inventory, create distribution requests, view personal stock
  - **DISTRIBUTOR**: Can approve/reject requests, manage all dealer inventories, complete distributions
- **Secure Authentication**: Cookie-based authentication with 8-hour session timeout
- **Default Credentials**:
  - Dealer: `dealer` / `Dealer@0`
  - Distributor: `distributor` / `Distributor@0`

### 🚗 Vehicle Management
- **Complete Vehicle Catalog**: Browse all EV models and trims with real-time pricing
- **Advanced Search**: Full-text search across model names, trim names, and descriptions
- **Detailed Specifications**: Comprehensive vehicle specs (battery, range, performance, dimensions)
- **Price History Tracking**: Historical price records with effective dates
- **Vehicle Comparison**: Side-by-side comparison of up to multiple vehicles with spec matrix
- **CRUD Operations**: Full create, read, update operations for models and trims
- **Status Management**: Track vehicle availability (Available, Unavailable, Discontinued, Upcoming)

### 🏢 Dealer Management
- **Dealer Profiles**: Complete dealer information and address management
- **Inventory Assignment**: Track which dealers carry which vehicle models
- **Stock Level Monitoring**: Real-time inventory levels per dealer per vehicle

### 📦 Inventory & Distribution System
- **Real-time Stock Tracking**: Live inventory management with quantity updates
- **Distribution Workflow**:
  1. **Request Creation**: Dealers submit distribution requests for specific vehicles
  2. **Pending Review**: Distributors review and validate requests
  3. **Approval/Rejection**: Distributors approve with quantities or reject requests
  4. **Completion**: Approved requests update dealer inventory automatically
- **Request States**: Pending → Approved/Rejected → Completed
- **Inventory Grouping**: View stock by model or by individual trims
- **Transactional Updates**: Atomic inventory updates with rollback capability

### 🔍 Advanced Features
- **Smart Search**: Search across vehicle models, trims, and specifications
- **Filtering & Sorting**: Multiple filter options and sorting capabilities
- **Model-based Inventory**: Group inventory by vehicle model for better management
- **Specification Management**: Detailed technical specifications for each vehicle
- **Cross-role Visibility**: Distributors can view all dealer inventories

## 🚀 Getting Started

### Prerequisites

- **.NET 9.0 SDK** or later
- **Microsoft SQL Server** (2019 or later recommended)
- **Visual Studio 2022** or **Visual Studio Code** with C# extensions

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd ASM_01
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Database Setup**
   - Ensure SQL Server is running
   - Update connection string in `ASM_01.DataAccessLayer/appsettings.db.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=ASM_01;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

4. **Apply Database Migrations**
   ```bash
   cd ASM_01.DataAccessLayer
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   cd ../ASM_01.WebApp
   dotnet run
   ```

6. **Access the application**
   - Open browser and navigate to `https://localhost:5001` or `http://localhost:5000`
   - Use the following test credentials:
     - **Dealer Account**: `dealer` / `Dealer@0`
     - **Distributor Account**: `distributor` / `Distributor@0`
   - Database includes seeded sample data for immediate testing

## 📁 Project Structure

```
ASM_01/
├── ASM_01.BusinessLayer/           # Business logic and services
│   ├── DTOs/                      # Data Transfer Objects
│   ├── Services/                  # Business services
│   │   ├── Abstract/             # Service interfaces
│   │   ├── DealerInventoryService.cs
│   │   ├── DistributionManagementService.cs
│   │   ├── SimpleAuthService.cs
│   │   └── VehicleService.cs
│   └── ASM_01.BusinessLayer.csproj
│
├── ASM_01.DataAccessLayer/        # Data access and persistence
│   ├── Entities/                  # Database entities
│   │   ├── VehicleModels/        # Vehicle-related entities
│   │   └── Warehouse/            # Dealer and stock entities
│   ├── Migrations/               # EF Core migrations
│   ├── Persistence/              # DbContext and configurations
│   ├── Repositories/             # Data repositories
│   │   ├── Abstract/             # Repository interfaces
│   │   ├── DealerRepository.cs
│   │   ├── DistributionRequestRepository.cs
│   │   ├── StockRepository.cs
│   │   ├── UnitOfWork.cs
│   │   └── VehicleRepository.cs
│   └── ASM_01.DataAccessLayer.csproj
│
└── ASM_01.WebApp/                # ASP.NET Core web application
    ├── Controllers/              # MVC Controllers
    │   ├── AuthController.cs
    │   ├── HomeController.cs
    │   ├── InventoryController.cs
    │   ├── RequestController.cs
    │   ├── StockController.cs
    │   └── VehicleController.cs
    ├── Models/                   # View models
    ├── Views/                    # Razor views
    ├── wwwroot/                  # Static assets
    └── Program.cs
```

## 🗄️ Database Schema

The system uses a comprehensive relational database with the following key entities:

### Core Vehicle Entities
- **EvModel**: Electric vehicle models with name, description, and status
- **EvTrim**: Vehicle trim levels (e.g., "Base", "Premium", "Sport") with model year and specifications
- **TrimPrice**: Historical pricing records with effective dates for each trim
- **TrimSpec**: Detailed technical specifications for each trim (battery capacity, range, performance, etc.)

### Business Entities
- **Dealer**: Dealership information including name, address, and contact details
- **VehicleStock**: Inventory tracking linking dealers to specific vehicle trims with quantities
- **DistributionRequest**: Request workflow management with status tracking (Pending → Approved/Rejected → Completed)

### Supporting Entities
- **SpecCategory**: Categories for vehicle specifications (Performance, Battery, Dimensions, etc.)
- **Spec**: Individual specification definitions (e.g., "Battery Capacity", "Range", "Top Speed")

### Relationships
- **EvModel** (1) → (Many) **EvTrim**
- **EvTrim** (1) → (Many) **TrimPrice** (historical)
- **EvTrim** (1) → (Many) **TrimSpec**
- **Dealer** (1) → (Many) **VehicleStock**
- **Dealer** (1) → (Many) **DistributionRequest**
- **EvTrim** (1) → (Many) **DistributionRequest**

## 🔒 Security Features

- **Authentication**: Cookie-based authentication with secure token management
- **Authorization**: Role-based access control (DEALER vs DISTRIBUTOR)
- **Session Management**: 8-hour configurable session timeout with sliding expiration
- **Input Validation**: Comprehensive model validation and business rule enforcement
- **SQL Injection Protection**: Parameterized queries via Entity Framework Core
- **Transaction Management**: Atomic operations with rollback capability for data consistency

## 🏛️ Architecture Patterns

### Business Logic Layer (`ASM_01.BusinessLayer`)
- **Service Layer**: Implements business logic and validation rules
- **DTO Pattern**: Data Transfer Objects for clean API contracts
- **Repository Pattern**: Abstracts data access operations
- **Unit of Work**: Manages database transactions and consistency

### Data Access Layer (`ASM_01.DataAccessLayer`)
- **Entity Framework Core**: ORM with code-first migrations
- **Repository Interfaces**: Defines data access contracts
- **Migration Management**: Database schema versioning and seeding
- **Connection Management**: Configurable database connections

### Key Design Principles
- **Separation of Concerns**: Clear boundaries between layers
- **Dependency Injection**: Loose coupling and testability
- **Async/Await**: Non-blocking I/O operations throughout
- **Error Handling**: Comprehensive exception management and logging

## 👥 Team & Contributors

This project was developed by a team of 6 contributors as part of the PRN222 course:

- Tran Nguyen Viet Thanh - Dev 1
- Lai Thanh Hung - Dev 2
- Nguyen Tan Phat - Dev 3
- Chu Tuan Kiet - Dev 4
- Tran Hoang Tuan - Dev 5
- Doan Le Thanh Trung - Dev 6

## 📚 API Documentation

The application provides comprehensive web interface through MVC controllers with role-based access:

### 🔐 Authentication Controller (`/Auth/`)
- `GET /Auth/Login` - Display login form
- `POST /Auth/Login` - Process user authentication
- `POST /Auth/Logout` - Sign out user
- `GET /Auth/Denied` - Access denied page

### 🚗 Vehicle Controller (`/Vehicle/`) - *All Authenticated Users*
- `GET /Vehicle/Catalog` - Browse vehicle catalog with optional search
- `GET /Vehicle/Details/{id}` - View detailed vehicle specifications
- `GET /Vehicle/Comparison?ids=1&ids=2` - Compare multiple vehicles
- `GET /Vehicle/EditModel/{modelId}` - Edit vehicle model (Distributor only)
- `POST /Vehicle/EditModel` - Update vehicle model (Distributor only)
- `GET /Vehicle/CreateModel` - Create new vehicle model (Distributor only)
- `POST /Vehicle/CreateModel` - Save new vehicle model (Distributor only)
- `GET /Vehicle/CreateTrim/{modelId}` - Create new trim for model (Distributor only)
- `POST /Vehicle/CreateTrim` - Save new vehicle trim (Distributor only)

### 📦 Request Controller (`/Request/`) - *Role-based Access*
**Dealers:**
- `GET /Request/MyRequests` - View personal distribution requests
- `GET /Request/Create` - Create new distribution request
- `POST /Request/Create` - Submit distribution request

**Distributors:**
- `GET /Request/Pending` - View all pending requests
- `GET /Request/Index` - View all requests (all statuses)
- `POST /Request/Approve` - Approve pending request with quantity
- `POST /Request/Reject` - Reject pending request
- `POST /Request/Complete` - Complete approved request (updates inventory)

### 📦 Stock Controller (`/Stock/`) - *Role-based Access*
**Dealers:**
- `GET /Stock/` - View personal inventory

**Distributors:**
- `GET /Stock/Dealer/{dealerId}` - View specific dealer inventory
- `GET /Stock/Details/{trimId}` - Redirect to vehicle details

### 🏢 Inventory Controller (`/Inventory/`) - *Distributor Only*
- `GET /Inventory/` - View all dealers for inventory management

## 📄 License

This project is developed for educational purposes as part of PRN222 course requirements.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📞 Support

For support and questions regarding this project, please contact the development team or refer to the project documentation.

---

**Built with ❤️ by Team ASM_01 for PRN222 Course**
