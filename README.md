# BlazorFullStackCrud

BlazorFullStackCrud is a full-stack web application built with Blazor using .NET 6. This project demonstrates CRUD (Create, Read, Update, Delete) operations using Blazor.

## Prerequisites

Before you begin, ensure you have met the following requirements:
* You have installed the .NET 6 SDK from [.NET 6 downloads](https://dotnet.microsoft.com/download/dotnet/6.0).
* You have a Windows/Linux/Mac machine.
* You are familiar with the basics of [Blazor](https://blazor.net) and ASP.NET Core.

## Setting Up

To set up the BlazorFullStackCrud project, follow these steps:

1. Clone the repository:
  git clone https://github.com/Abhisheak-Amalraj/BlazorFullStackCrud.git

2. Navigate to the project directory:
  cd BlazorFullStackCrud
  
3. Configuring:
  To configure the project, you may need to:

  Update the appsettings.json with your database connection strings and other necessary configurations.

  If there are any pending migrations, apply them to your database:
    dotnet ef database update
  
4. Running the project:
  To run the project, follow these steps:

  Build the project to ensure all dependencies are properly restored:
    dotnet build
  Run the application:
    dotnet run

