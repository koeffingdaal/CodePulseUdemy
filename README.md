CodePulseUdemy
This repository contains the source code and resources for the CodePulse course on Udemy. The course focuses on building a full-stack web application using ASP.NET Core for the backend and Angular for the frontend.

Project Structure
The repository is organized into two main projects:

CodePulse.API: The backend project developed with ASP.NET Core Web API.

CodePulse.UI: The frontend project developed with Angular.
GitHub
+5
GitHub
+5
GitHub
+5

Getting Started
Prerequisites
.NET 6 SDK or later

Node.js and npm

Angular CLI

SQL Server (or another supported database)

Backend Setup
Navigate to the CodePulse.API directory:
GitHub

bash
Copy
Edit
cd CodePulse.API
Restore the NuGet packages:

bash
Copy
Edit
dotnet restore
Apply database migrations:

bash
Copy
Edit
dotnet ef database update
Run the API:
GitHub

bash
Copy
Edit
dotnet run
Frontend Setup
Navigate to the CodePulse.UI directory:
GitHub
+2
GitHub
+2
GitHub
+2

bash
Copy
Edit
cd CodePulse.UI
Install the dependencies:

bash
Copy
Edit
npm install
Run the Angular application:

bash
Copy
Edit
ng serve
Open your browser and navigate to http://localhost:4200 to view the application.

Features
User registration and authentication

JWT-based authorization

CRUD operations for various entities

Responsive UI with Angular Material

Integration with SQL Server

Contributing
Contributions are welcome! Please fork the repository and submit a pull request for any enhancements or bug fixes.
