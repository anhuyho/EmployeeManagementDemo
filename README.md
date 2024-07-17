# Employee Management System

This repository contains a complete solution for managing employees: a .Net Core API (EmployeeService), a .Net Core hosted service (HostedService), and an Angular frontend application. This system allows you to perform various operations on employee data, such as creating, updating, deleting, and viewing employee details.

## Backend

### Employee Service - Employee Management API

Employee Service is a .Net Core API that handles employee management. It follows best practices and design principles to ensure code quality and maintainability.
![image](https://github.com/anhuyho/TecAlliance/assets/5900818/908f3d2d-2d0a-4ce0-a426-4d1d47c38e94)

#### Features

- **Employee Data**:
  - Employees are identified by an `id` and have attributes including `name,` `position,` `hiring date,` and `salary.`
  
- **Endpoints**:
  - The API provides the following endpoints:
    - `GET /employee`: Retrieve a list of all employees.
    - `GET /employee/{id}`: Retrieve an employee by their ID.
    - `POST /employee`: Create a new employee.
    - `PUT /employee/{id}`: Update an existing employee by their ID.
    - `DELETE /employee/{id}`: Delete an employee by their ID.
    
- **Design Principles**:
  - Use Clean Architecture
  - Apply the CQRS pattern using MediatR
  - Follow Domain-Driven Design (DDD)
  - Apply SOLID, KISS, and YAGNI principles for clean and maintainable code.
  
- **Data Storage**:
  - Employee data is stored in a JSON file.
  
- **Caching**:
  - Use .NET InMemmory Cache and apply DI to replace it with another Cache like Redis.
  
- **Logging**:
  - Use Serilog to log console and file, and be able to extend it to write on other platforms like DataDog.
  
- **Dependency Injection**:
  - Apply for Services and repositories, ... be able to replace, change the implementation or technical easily
  
- **Configuration**:
  - Configuration options, such as the file path of the employee repository, are abstracted using configuration settings.
  
- **Unit Tests**:
  - Use xUnit to write the Unit Test (for Business (Application and Domain layers)) and Integration Test (for API, etc.).

### Report Service - Employee Reporting Service

Report Service is a .Net Core application with a hosted service that periodically generates reports of employees by requesting data from the Employee Management API.
![image](https://github.com/anhuyho/TecAlliance/assets/5900818/d8fded07-e108-43d0-8fc5-09cb477c4070)

#### Features

- **Reporting Service**:
  - The hosted service periodically requests employee data from the API and generates reports.
  
- **Configuration**:
  - Configuration options, such as the URI of the Employee Management API, and a period of time (second) are abstracted using configuration settings.

## Frontend - Angular Employee Management Application

The front end is an Angular application that allows users to manage employees using the Employee Management API.
![image](https://github.com/anhuyho/TecAlliance/assets/5900818/cfbe960f-d3f4-4269-8db5-046a0eda09de)
![image](https://github.com/anhuyho/TecAlliance/assets/5900818/30519491-496e-4109-a2a4-935d084eff7b)

#### Features

- **API Integration**:
  - The application integrates with the Employee Management API to perform various operations.
  - Use HttpClient with Observable
  
- **Bearer Token**:
  - The application simulates including a bearer token in API requests
  - Use Interceptor
  
- CRUD operations
  - Use Reative Form for List, Create, Edit form
  - Write custom validation to validate control
  

## How to Run
To run easily, we can use Visual Studio and Angular CLI to serve/build
(or we can run/deploy each service/app by dotnet CLI and Angular CLI)
1. Clone Repo
2. Use Visual Studio to open the solution file **TecAlliance.sln**
3. Setting Run Multiple Projects in Visual Studio
4. Run the project using Visual Studio
5. Run the Angular app by open **FrontEnd\employees-admin** and run / build by angular cli

![image](https://github.com/anhuyho/TecAlliance/assets/5900818/39022da3-d2bc-42ce-aba8-95a59ab8478f)
