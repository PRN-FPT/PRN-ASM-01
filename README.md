# EV Retail Management System (ASM_01)

A comprehensive Electric Vehicle (EV) retail management system built with ASP.NET Core MVC, designed to facilitate vehicle distribution between distributors and dealers.

## 📋 Table of Contents

- [Project Overview](#project-overview)
- [Usage](#usage)
- [Features](#features)
- [Architecture](#architecture)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Installation & Setup](#installation--setup)
- [Project Structure](#project-structure)
- [Database](#database)
- [Contributors](#contributors)
- [License](#license)

## 🚗 Project Overview

This system manages the complete lifecycle of electric vehicle retail operations, from vehicle catalog management to distribution between distributors and dealers. The application supports two main user roles:

- **Distributors**: Manage vehicle inventory, approve/reject dealer requests, and maintain vehicle catalogs
- **Dealers**: Browse vehicle catalogs, create distribution requests, and manage their stock

## 🔑 Usage

### Default User Accounts

The system comes with pre-seeded user accounts for testing:

**Distributor Account:**
- Username: `distributor`
- Password: `Distributor@0`
- Role: DISTRIBUTOR

**Dealer Account:**
- Username: `dealer`
- Password: `Dealer@0`
- Role: DEALER

### Getting Started

1. **Login**: Use one of the default accounts above
2. **Browse Vehicles**: View the vehicle catalog and search for specific models
3. **Compare Vehicles**: Select multiple vehicles to compare their specifications
4. **Manage Inventory** (Distributor only): View dealer inventories and manage stock
5. **Handle Requests**:
   - Dealers: Create distribution requests for desired vehicles
   - Distributors: Approve, reject, or complete pending requests

### Key Workflows

#### For Dealers:
1. Browse vehicle catalog
2. Create distribution requests for desired vehicles
3. Monitor request status in "My Requests"

#### For Distributors:
1. Review pending requests in "Pending Requests"
2. Approve/reject requests based on availability
3. Complete approved requests to update stock levels
4. Manage vehicle models and trims

## ✨ Features

### Vehicle Management
- **Vehicle Catalog**: Browse and search electric vehicle models and trims
- **Vehicle Comparison**: Compare specifications of multiple vehicles side-by-side
- **Vehicle Details**: View comprehensive vehicle specifications and pricing
- **Model & Trim Management**: Create and edit vehicle models and trims (Distributor only)

### Inventory Management
- **Dealer Inventory**: View and manage dealer stock levels
- **Stock Tracking**: Monitor vehicle quantities across the distribution network

### Distribution System
- **Distribution Requests**: Dealers can request vehicles from distributors
- **Request Management**: Distributors can approve, reject, or complete requests
- **Request History**: Track all distribution requests and their statuses

### User Management
- **Role-based Authentication**: Secure login system with DISTRIBUTOR and DEALER roles
- **User Sessions**: Cookie-based authentication with configurable expiration

### Additional Features
- **Search Functionality**: Search vehicles by name or specifications
- **Responsive UI**: Clean, modern web interface
- **Data Validation**: Comprehensive input validation and error handling

## 🏗️ Architecture

This project follows a **multi-layer architecture** pattern:

```
┌─────────────────┐
│   Web Layer     │  ← ASP.NET Core MVC (Controllers, Views)
├─────────────────┤
│ Business Layer  │  ← Services, DTOs, Mappers
├─────────────────┤
│  Data Layer     │  ← Entity Framework Core, Repositories
└─────────────────┘
```

### Layer Responsibilities

- **Web Layer (ASM_01.WebApp)**: Handles HTTP requests, user interface, and presentation logic
- **Business Layer (ASM_01.BusinessLayer)**: Contains business logic, services, and data transfer objects
- **Data Access Layer (ASM_01.DataAccessLayer)**: Manages database operations, entities, and repository patterns

## 🛠️ Technologies Used

- **Framework**: ASP.NET Core 9.0
- **Architecture**: MVC (Model-View-Controller)
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Authentication**: ASP.NET Core Identity with Cookie Authentication
- **Frontend**: Razor Views, HTML, CSS, JavaScript
- **Language**: C#
- **IDE**: Visual Studio 2022

## 📋 Prerequisites

Before running this application, ensure you have the following installed:

### Required Software

- **.NET 9.0 SDK** or later - [Download here](https://dotnet.microsoft.com/download/dotnet/9.0)
- **SQL Server** (choose one option):
  - **SQL Server LocalDB** (Recommended for development) - Lightweight, automatically installed with Visual Studio
  - **SQL Server Express** (Free edition) - [Download here](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
  - **Full SQL Server** (Developer edition) - [Download here](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- **Visual Studio 2022** (recommended) or any C# IDE - [Download here](https://visualstudio.microsoft.com/downloads/)
- **Git** for version control - [Download here](https://git-scm.com/downloads)

### System Requirements

- **Operating System**: Windows 10/11, macOS, or Linux
- **Memory**: Minimum 4GB RAM (8GB recommended)
- **Storage**: At least 2GB free space for the project and SQL Server

> **Note**: No manual database creation or SQL script execution is required. The application will automatically create and seed the database on first run.

## 🚀 Installation & Setup

### 1. Clone the Repository

```bash
git clone <repository-url>
cd ASM_01
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Database Setup

#### Automatic Database Creation (No Manual Setup Required!)

The application uses **Entity Framework Core** with automatic migrations and database seeding. **No manual SQL scripts or database creation is needed!**

#### Option A: Using SQL Server LocalDB (Recommended for development)

1. **Install SQL Server LocalDB** (automatically included with Visual Studio 2022)
2. **Run the application** - The database `EVRetailsDb` will be created and seeded automatically on first startup
3. **No connection string changes needed** - Uses the default LocalDB configuration

#### Option B: Using SQL Server (Full installation)

If you prefer using a full SQL Server instance:

1. **Install SQL Server** from Microsoft (any edition works)
2. **Optional**: Update the connection string in `ASM_01.DataAccessLayer/appsettings.db.json` if using a different server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=EVRetailsDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

3. **Run the application** - Database and tables will be created automatically via Entity Framework migrations

> **Important**: The application will automatically run migrations and seed sample data (vehicles, users, dealers) on the first run. No manual intervention required!

### 4. Build the Application

```bash
dotnet build
```

### 5. Run the Application

```bash
dotnet run --project ASM_01.WebApp
```

The application will be available at `https://localhost:5001` or `http://localhost:5000`

## 📁 Project Structure

```
ASM_01/
├── ASM_01.BusinessLayer/           # Business Logic Layer
│   ├── DTOs/                      # Data Transfer Objects
│   ├── Mappers/                   # Object mapping configurations
│   ├── Services/                  # Business logic services
│   └── ASM_01.BusinessLogicLayer.csproj
├── ASM_01.DataAccessLayer/        # Data Access Layer
│   ├── Entities/                  # Database entities
│   ├── Persistence/               # DbContext and configurations
│   ├── Repositories/              # Data access repositories
│   └── ASM_01.DataAccessLayer.csproj
├── ASM_01.WebApp/                 # Web Application Layer
│   ├── Controllers/               # MVC Controllers
│   ├── Models/                    # View Models
│   ├── Views/                     # Razor Views
│   ├── wwwroot/                   # Static assets (CSS, JS, images)
│   └── ASM_01.WebApp.csproj
└── ASM_01.sln                     # Solution file
```

## 🗄️ Database

### Database Schema

The system uses the following main entities:

- **EvModel**: Electric vehicle models (e.g., Tesla Model Y, VinFast VF8)
- **EvTrim**: Specific trim levels of vehicle models
- **TrimPrice**: Pricing information for vehicle trims
- **Spec**: Vehicle specifications (battery capacity, range, etc.)
- **TrimSpec**: Links specifications to specific trims
- **Dealer**: Dealership information
- **User**: User accounts with role-based access
- **VehicleStock**: Inventory levels at each dealer
- **DistributionRequest**: Vehicle distribution requests from dealers

### Sample Data

The database is pre-seeded with sample data including:
- Vehicle models (VinFast VF8, Tesla Model Y)
- Various trim levels with specifications
- Sample dealers and users
- Initial stock levels

## 👥 Contributors

**Team Leader:**
- Tran Nguyen Viet Thanh

**Team Members:**
- Nguyen Tan Phat
- Lai Thanh Hung
- Chu Tuan Kiet
- Tran Hoang Tuan
- Doan Le Thanh Trung

## 📄 License

This project is developed for educational purposes as part of PRN222 course assignment.

---

*For questions or support, please contact the development team.*
