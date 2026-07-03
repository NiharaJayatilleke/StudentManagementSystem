# StudentManagementSystem

# Student Management System

A full-stack Student Management System built using **Angular 22** and **ASP.NET Core Web API** following a layered architecture. The application allows administrators to manage students, subjects, and enrollments while providing authentication, authorization, validation, notifications, and a modern user interface.

---

## Features

### Authentication

* User registration
* User login using JWT authentication
* Protected routes using Angular Route Guards
* Automatic JWT attachment using an HTTP Interceptor
* Automatic logout on unauthorized requests

### Student Management

* View all students
* Add new students
* Edit existing students
* Delete students
* Reactive Forms with client-side validation
* Async validation for duplicate Student IDs
* Pagination support

### Subject Management

* View all subjects
* Add new subjects
* Edit existing subjects
* Delete subjects

### Enrollment Management

* Assign subjects to students
* Remove assigned subjects
* Prevent duplicate enrollments
* Display current enrollments

### User Experience

* Angular Material Snackbar notifications
* Angular Material confirmation dialogs
* Global loading indicator using HTTP Interceptor
* Font Awesome icons
* Responsive and consistent UI styling

---

## Technology Stack

### Frontend

* Angular 22
* TypeScript
* Angular Router
* Reactive Forms
* Angular Material
* RxJS
* Font Awesome

### Backend

* ASP.NET Core Web API
* Entity Framework Core
* SQLite
* JWT Authentication
* BCrypt Password Hashing

---

## Architecture

The backend follows **Onion Architecture**, separating responsibilities into multiple layers:

```text
Presentation (Controllers)
        ↓
Application (Services)
        ↓
Infrastructure (Repositories)
        ↓
Database (Entity Framework Core)
```

This separation improves maintainability, scalability, and testability.

---

## Validation

### Frontend

* Reactive Forms
* Required field validation
* Range validation
* Length validation
* Async Student ID uniqueness validation

### Backend

* Data Annotation validation
* Model validation using `[ApiController]`
* Business rule validation in the service layer

---

## Security

* JWT Bearer Authentication
* Password hashing using BCrypt
* Protected API endpoints with `[Authorize]`
* Route Guards for protected pages
* Automatic token injection using HTTP Interceptor

---

## Project Structure

```text
StudentManagement/
│
├── new-ui/                  Angular Frontend
│
│   ├── auth/
│   ├── students/
│   ├── subjects/
│   ├── enrollments/
│   ├── services/
│   ├── guards/
│   ├── interceptors/
│   ├── validators/
│   └── shared/
│
└── TestProject/             ASP.NET Core Backend
    ├── Controllers/
    ├── DTOs/
    ├── Services/
    ├── Repositories/
    ├── Models/
    ├── Data/
    └── Migrations/
```

---

## Running the Application

### Backend

Navigate to the backend project:

```bash
cd TestProject
```

Restore packages:

```bash
dotnet restore
```

Apply migrations:

```bash
dotnet ef database update
```

Run the API:

```bash
dotnet run
```

The API will be available at:

```
http://localhost:5000
```

---

### Frontend

Navigate to the Angular project:

```bash
cd new-ui
```

Install dependencies:

```bash
npm install
```

Start the development server:

```bash
ng serve
```

Open:

```
http://localhost:4200
```

---

## API Overview

### Authentication

* POST `/api/auth/register`
* POST `/api/auth/login`

### Students

* GET `/api/students`
* GET `/api/students/{id}`
* POST `/api/students`
* PUT `/api/students/{id}`
* DELETE `/api/students/{id}`
* GET `/api/students/exists?studentId=`

### Subjects

* GET `/api/subjects`
* GET `/api/subjects/{id}`
* POST `/api/subjects`
* PUT `/api/subjects/{id}`
* DELETE `/api/subjects/{id}`

### Enrollments

* GET `/api/enrollments`
* POST `/api/enrollments`
* DELETE `/api/enrollments`

---

## Future Improvements

* Search and filtering
* Sorting
* Role-based authorization
* Refresh Tokens
* Unit and Integration Testing
* Docker support
* CI/CD Pipeline
* Audit logging
* Email verification
* Password reset
* Dashboard with statistics

---

## Learning Outcomes

This project demonstrates experience with:

* Angular standalone components
* Reactive Forms
* Custom Async Validators
* HTTP Interceptors
* Route Guards
* Angular Material
* RESTful API development
* JWT Authentication
* Entity Framework Core
* Repository Pattern
* Onion Architecture
* DTO Mapping
* Backend and frontend validation
* Clean application architecture



## Author

Developed as a learning project to practice modern full-stack web development using Angular and ASP.NET Core.
