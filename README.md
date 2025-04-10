
# Team Counter API

## Overview
The **Team Counter API** is a service for tracking and managing step counts for teams within an organization.

## Features
- **Create a new team**: Allows users to create new teams by providing a name.
- **Add a counter to a team**: Allows users to add step counters for individual team members.
- **Increment a counter**: Allows users to increment the step count of a particular counter.
- **Get total steps of a team**: Allows users to retrieve the total steps taken by all members of a team.
- **Get a leaderboard**: Displays the total steps taken by all teams for comparison.
- **Get counters of a team**: Retrieves the list of counters (team members) and their respective step counts.
- **Delete a team**: Allows users to delete a team and all its associated counters.
- **Delete a counter**: Allows users to delete a specific counter from a team.

## API Documentation

The API provides several endpoints for managing teams and their step counters. You can view the full API documentation through the Swagger UI after running the service.

### Endpoints

#### 1. **Create Team**
- **POST** `/teams`
- Request Body:
  ```json
  {
    "name": "Team A"
  }
  ```
- Response:
  ```json
  {
    "teamId": "eeb5f914-f14a-45c0-bc29-5458a52b1b2b"
  }
  ```

#### 2. **Add Counter**
- **POST** `/teams/{teamId}/counters`
- Request Body:
  ```json
  {
    "name": "John Doe"
  }
  ```
- Response:
  ```json
  {
    "counterId": "b3efb0ad-f5a7-4f6c-a9b0-0a01312ef5fc"
  }
  ```

#### 3. **Increment Counter**
- **POST** `/teams/{teamId}/counters/{counterId}`
- Request Body:
  ```json
  {
    "steps": 1000
  }
  ```
- Response: `204 No Content`

#### 4. **Get Team Total**
[...]

## Technologies Used
- **.NET 8**
- **MediatR**
- **FluentValidation**
- **Swashbuckle (Swagger)**
- **In-Memory Data Store**

## Unit Testing

Unit tests for the API can be found in the `/tests` directory. The tests use **XUnit** and **Moq** for mocking dependencies.

### Running Unit Tests
```bash
dotnet test
```