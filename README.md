# Candidate Hub

Welcome to Candidate Hub! This project is built with clean architecture principles, aiming for separation of concerns and maintainability. It includes unit tests for services, making it easy to validate functionality and perform regression testing.

## Technology Stack

- **.NET 7:** The primary framework used for development.
- **Fluent Validation:** Employed for input and business logic validation.
- **Entity Framework Core:** Used as the Object-Relational Mapping (ORM) tool.
- **NUnit Test Framework:** Utilized for unit testing.
- **SQL Server:** Selected as the database solution.

## Project Structure

- **candidatehub.Web:** This API project contains migration and middleware configurations.
- **candidatehub.Presentation:** Contains API endpoints and database migration scripts.
- **candidatehub.Application:** Holds services responsible for business logic, validation, and mapping.
- **candidatehub.Domain:** Houses entities, representing core business objects.
- **candidatehub.Infrastructure:** Responsible for accessing database objects.
- **candidatehub.Test:** Consists of NUnit tests for each service.

## Dependency Structure

- **Domain:** Acts as the core project.
- **Application:** References the Domain project.
- **Infrastructure:** Also references the Domain project.
- **Presentation:** References the Application project.
- **Web:** References both the Presentation and Infrastructure layers.

## Running the Project

To run the project successfully, follow these steps:

1. Define connection strings for the databases used by each layer.
2. Update the database migrations located in the API project.
3. Ensure all dependencies are installed and configured correctly.
4. Set candidatehub.Web as a startup project, then run the project.

## Unit Testing

We've included unit tests to ensure code reliability:

- **Shouldly Library:** Used for assertion.
- **Moq:** Utilized for creating mock objects.
- **Fixture:** Assists in creating objects for testing.

