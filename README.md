# My ASP.NET Core Web API Project

## Description
This is a sample ASP.NET Core Web API project that provides endpoints for managing users.

## Installation
1. Clone this repository.
2. Navigate to the project directory.
3. Open the project in Visual Studio.
4. Click on the "Start" button in Visual Studio to run the project.

## Usage
- Two windows will automatically open:
  - Swagger UI: Explore and test the API endpoints.
  - MVC View: Navigate to `/Home/Index` to view the user interface.
- Method: `GET`
- Description: Get a list of all users.
- Example: `curl http://localhost:5015/api/Patients`

## Endpoints Documentation
### GET /api/Patients
- Description: Get a list of all users.
- Response: JSON array of user objects.

### POST /api/Patients
- Description: Create a new user.
- Request Body: JSON object with user data.
- Response: JSON object with the newly created user.

## Hosted Services
- The project includes hosted services for database migrations and automatic opening of Swagger UI and MVC View windows.

## Contributing
Contributions are welcome! Please submit bug reports or feature requests via GitHub issues.

## Contact
For questions or feedback, contact https://github.com/zahirulislam478
