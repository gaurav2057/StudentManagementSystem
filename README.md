# 🎓 Student Management System

A Student Management System built using **ASP.NET Core MVC**, **Dapper**, and **SQL Server**. This project demonstrates user authentication, role-based authorization, and complete CRUD operations for managing student records.

---

## 🚀 Features

### Authentication
- User Registration
- User Login
- Secure Password Hashing
- Cookie-Based Authentication
- Logout Functionality

### Authorization
- Role-Based Access Control
- Admin and User Roles
- Protected Student Module using `[Authorize]`
- Only Admins can delete student records

### Student Management
- Add Student
- View Students
- Edit Student Details
- Delete Student (Admin Only)
- Search Students

### Validation
- Client-side Validation
- Server-side Validation
- Duplicate Email Check During Registration

---

## 🛠 Tech Stack

- ASP.NET Core MVC (.NET 10)
- C#
- Dapper ORM
- SQL Server
- Bootstrap 5
- HTML5
- CSS3
- Razor Views

---

## 📂 Project Structure

```
StudentManagement
│
├── Controllers
├── Models
├── Views
├── Repositories
├── Data
├── wwwroot
├── Database
│   └── StudentDB.sql
├── appsettings.json
└── Program.cs
```

---

## 🗄 Database

The project uses two tables:

### Users

- UserId
- FullName
- Email
- PasswordHash
- Role
- CreatedAt

### Students

- StudentId
- Name
- Age
- Gender
- Course
- City

---

## 🔐 Roles

### User
- Register
- Login
- View Students
- Add Students
- Edit Students

### Admin
- All User Permissions
- Delete Student Records

---

## 📚 Concepts Used

- ASP.NET Core MVC
- Repository Pattern
- Dapper ORM
- Dependency Injection
- Cookie Authentication
- Role-Based Authorization
- Model Validation
- SQL Server Integration
- Razor Views
- Bootstrap 5

---

## 👨‍💻 Author

**Gaurav Singh**

GitHub: https://github.com/gaurav2057
