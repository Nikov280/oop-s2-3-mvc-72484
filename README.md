# VGC Management System - Academic Administration Portal

A robust academic management system built with **ASP.NET Core MVC**. This application allows administrators and faculty to manage campus branches, academic courses, student profiles, faculty staff, and course enrollments.

---

## 🚀 Mandatory Non-Functional Requirements Implemented

- **Server-Side Authorization:** Enforced using `[Authorize]` attributes at the controller and action levels. UI links are dynamically hidden based on user roles, but direct URL access is also strictly blocked by the server.
- **Validation:** Implemented via **Data Annotations** in the domain models (e.g., `[Required]`, `[StringLength]`, `[EmailAddress]`). Additionally, custom server-side checks verify business logic, such as ensuring a course's end date is after its start date.
- **Error Handling:** Configured a global exception handling middleware in `Program.cs` to redirect users to a custom, friendly error page, preventing raw exception data or stack traces from leaking to the end user.
- **Seed/Mock Data:** The system includes a `DbInitializer` class that automatically populates the database with roles, test users, and sample business data (Branches, Courses, Students, etc.) immediately after the database is created.

---

## 👤 Test Accounts (Seed Data)

Use the following credentials to test different authorization levels:

| Role | Email | Password | Access Level |
| :--- | :--- | :--- | :--- |
| **Admin** | `admin@vgc.ie` | `Admin123!` | Full System Access |
| **Faculty** | `teacher@vgc.ie` | `Faculty123!` | Manage Students & Courses |
| **Student** | `student@vgc.ie` | `Student123!` | View Personal Info & Courses |

---

## 🛠️ Setup and Installation

### 1. Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or LocalDB.

### 2. Database Configuration
1. Open the solution in **Visual Studio**.
2. Check the connection string in `appsettings.json`. It is configured by default for `(localdb)\\mssqllocaldb`.
3. Open the **Package Manager Console** and run:
   ```powershell
   Update-Database