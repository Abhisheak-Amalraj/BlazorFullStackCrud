Everflow Water Consumption Tracker
This repository contains a basic Water Consumption Tracker application built using C# .NET 6, Entity Framework Core, MS SQL, and a Blazor-based front-end UI.

The project demonstrates adherence to SOLID principles, test-driven development (TDD), and follows best practices for creating scalable and maintainable software.

Table of Contents
Getting Started
Prerequisites
Cloning the Repository
Project Structure
Features
Persistence Layer
REST API
Blazor-Based UI
Running the Application
Database Setup
Migration and Seeding
Running the API and Front-End
API Endpoints
User Endpoints
Water Intake Endpoints
Testing
Unit Tests
Integration Tests
Version Control
Future Improvements
Getting Started
Prerequisites
To run this project, you need the following tools:

.NET 6 SDK (Download Here)
MS SQL Server (Download Here)
A code editor like Visual Studio or VS Code
Entity Framework Core (installed via NuGet)
Cloning the Repository
To clone this repository, run:

bash
Copy code
git clone <repository-url>
cd <repository-folder>
Project Structure
plaintext
Copy code
├── Controllers
│   ├── UserController.cs
│   └── WaterIntakeController.cs
├── Data
│   └── ApplicationDbContext.cs
├── Entities
│   ├── User.cs
│   └── WaterIntake.cs
├── Repositories
│   └── IUserRepository.cs
├── Services
│   └── UserService.cs
├── Tests
│   ├── UnitTests
│   └── IntegrationTests
└── UI
    └── BlazorFrontEnd
Features
Persistence Layer
The persistence layer includes the following entities:

User:

ID (integer)
Firstname (string)
Surname (string)
Email (string)
Water Intake:

ID (integer)
UserID (integer)
IntakeDate (datetime)
Consumed Water (integer in millilitres)
REST API
The REST API provides the following features:

User Management:
Fetch a list of users
Create a new user
View user details by ID
Modify user information
Remove a user
Water Intake Management:
Obtain a user’s water intake records
Add new water intake records for a user
View specific water intake records by ID
Modify water intake records
Delete water intake records
Blazor-Based UI
The front-end UI is built using Blazor and provides the following functionalities:

Manage users (list, add, update, delete)
Manage water intake records (view, add, update, delete)
Running the Application
Database Setup
Create a local MS SQL Server database or connect to an existing one.
Update the appsettings.json file with your database connection string.
Migration and Seeding
To apply migrations and seed the database, run the following commands:

bash
Copy code
dotnet ef migrations add InitialCreate
dotnet ef database update
Running the API and Front-End
To run the API and front-end:

bash
Copy code
dotnet run
Navigate to:
https://localhost:<port> to access the Blazor front-end UI.

API Endpoints
User Endpoints
Method	Endpoint	Description
GET	/api/users	Fetch all users
POST	/api/users	Create a new user
GET	/api/users/{id}	Get user by ID
PUT	/api/users/{id}	Update user details
DELETE	/api/users/{id}	Remove a user
Water Intake Endpoints
Method	Endpoint	Description
GET	/api/waterintake/{userId}	Get water intake records by user
POST	/api/waterintake	Add a water intake record
GET	/api/waterintake/details/{id}	View specific intake record
PUT	/api/waterintake/{id}	Update water intake record
DELETE	/api/waterintake/{id}	Delete water intake record
Testing
Unit Tests
Unit tests validate the business logic of the application.

Integration Tests
Integration tests verify the communication between API components.

To run tests:

bash
Copy code
dotnet test
Version Control
This project uses Git for version control. All commits are pushed to a remote repository hosted on GitHub.

To contribute:

Fork the repository.

Create a feature branch:

bash
Copy code
git checkout -b feature-branch-name
Commit changes and push:

bash
Copy code
git add .
git commit -m "Your commit message"
git push origin feature-branch-name
Create a pull request on GitHub.

Future Improvements
Add authentication and role-based access control.
Implement user activity tracking for analytics.
Add email notifications for daily water intake summaries.
Implement Docker containers for easier deployment.
Thank you for reviewing this project! Feel free to








ChatGPT can make mistakes. Check important info.
