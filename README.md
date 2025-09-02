# Employees Management System

This project is an Employees Management System designed to help organizations efficiently manage employee records and streamline HR-related tasks. The system provides a user-friendly interface for administrators and HR personnel to add, update, and remove information and generate useful reports.

## Features

- **Department Management (CRUD)**: Create, read, update, and delete departments.
- **Employee Management (CRUD)**: Manage employees, including personal info, job roles, and contact details. Supports profile image/file uploads.
- **User Management (CRUD)**: Manage application users.
- **Role Management (CRUD)**: Create and assign roles; role-based authorization enforced.
- **Secure Authentication & Authorization**: ASP.NET Core Identity for user accounts and roles to protect sensitive data.
- **User-Friendly Interface**: Razor Views with Bootstrap for a clean, responsive UI.

## Technologies Used

- **.NET 9** (ASP.NET Core MVC)
- **Entity Framework Core 9** with **SQL Server**
- **ASP.NET Core Identity** for authentication/authorization
- **Razor Views** (CSHTML) + **Bootstrap** (via LibMan)
- **3-Teir architecture** : **PL** (Presentation), **BLL** (Business Logic), **DAL** (Data Access)

## Solution Structure

- `EmpsManagement.PL` (Presentation Layer): ASP.NET Core MVC app (controllers, views, static assets)
- `EmpsManagement.BLL` (Business Logic Layer): Services, DTOs, factories, email sender
- `EmpsManagement.DAL` (Data Access Layer): DbContext, EF Core configurations, repositories, Unit of Work, models, migrations



