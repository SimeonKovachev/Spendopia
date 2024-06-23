# Spendopia

## Table of Contents

1. [Introduction](#introduction)
2. [Features](#features)
3. [Getting Started](#getting-started)
   - [Prerequisites](#prerequisites)
   - [Installation](#installation)
   - [Configuration](#configuration)
4. [Usage](#usage)
5. [License](#license)

## Introduction

Spendopia is a financial management application designed to help users track their expenses and manage their budgets effectively. This repository contains the backend and service configurations for Spendopia.

## Features

- **Expense Tracking:** Record and categorize expenses for better financial insights.
- **Budget Management:** Set budgets and receive alerts when nearing limits.
- **Data Visualization:** View spending patterns through graphs and charts.
- **User Authentication:** Secure user accounts with authentication and authorization features.

## Getting Started

To get Spendopia up and running on your local machine, follow these steps:

### Prerequisites

Before starting, ensure you have the following installed:

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or [SQLite](https://www.sqlite.org/download.html)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/SimeonKovachev/Spendopia.git
   cd Spendopia
2. Restore dependencies and build the project:
    ```bash
    dotnet restore
    dotnet build
    
## Configuration

### Database Setup:

Update `appsettings.json` in Spendopia.API project with your database connection string.

### Initial Database Migration:

Run the following commands:

```bash
cd Spendopia.Data
dotnet ef database update
```

## Usage

### Run the Application:

Start the application using:
```bash
dotnet run --project Spendopia.API
```

## License
This project is licensed under the MIT License. See the LICENSE file for details.
