# RocketOps - Deployment Health Monitor

![RocketOps Logo](https://via.placeholder.com/200x100?text=RocketOps)

## 1. Project Overview

RocketOps is a comprehensive Deployment Health Monitoring system designed to provide real-time insights into the health and performance of applications in production environments. Built on a microservices architecture, RocketOps offers robust monitoring capabilities, intelligent alerting, and detailed reporting to ensure optimal application performance and reliability.

### Key Features

- **Real-time Monitoring**: Continuous tracking of application metrics, logs, and performance indicators
- **Intelligent Alerting**: Configurable thresholds with smart notification systems
- **Comprehensive Reporting**: Detailed performance analytics and trend analysis
- **Event-driven Architecture**: Responsive system design for real-time updates
- **Scalable Infrastructure**: Cloud-native approach for handling varying workloads

## 2. Technology Stack

RocketOps leverages modern technologies to deliver a robust and scalable monitoring solution:

| Component | Technology |
|-----------|------------|
| Backend Services | .NET 8 using C# |
| API Gateway | Ocelot |
| Frontend | React with TypeScript |
| Database | Azure CosmosDB |
| Communication | Event-driven messaging |
| Containerization | Docker & Docker Compose |
| Testing | xUnit, NSubstitute, Playwright |
| CI/CD | GitHub Actions |
| Monitoring | Application Insights |

## 3. Architecture

RocketOps follows a microservices architecture with clean architecture principles and CQRS pattern implementation. The system is designed to be highly scalable, maintainable, and resilient.

### Architecture Diagram

Refer to the architecture diagram for a visual representation of RocketOps components and their interactions.

### Components

#### 3.1 API Gateway (Ocelot)

The API Gateway serves as the entry point for all client requests, handling:

- Request routing
- Load balancing
- Authentication & authorization
- Rate limiting
- Request aggregation

#### 3.2 Microservices

##### 3.2.1 Monitoring.API

Responsible for collecting and processing metrics from deployed applications:

- Health check management
- Metric collection
- Performance tracking
- Resource utilization monitoring
- Log aggregation

##### 3.2.2 Alerts.API

Manages the alerting system:

- Alert rule configuration
- Threshold management
- Notification dispatch
- Alert history
- Escalation policies

##### 3.2.3 Reporting.API

Generates insights and reports:

- Historical data analysis
- Performance trending
- Custom report generation
- Scheduled reports
- Data visualization endpoints

#### 3.3 Shared Libraries

##### 3.3.1 Infrastructure Library

Contains cross-cutting concerns:

- Logging
- Authentication
- Caching
- Resilience patterns
- Configuration management

##### 3.3.2 Domain Library

Houses the core business logic and domain models:

- Entities
- Value objects
- Domain events
- Business rules
- Interfaces

##### 3.3.3 Data Library

Manages data access:

- Repository implementations
- Data mapping
- Query optimization
- Transaction management
- Database migrations

#### 3.4 Persistence Layer

Azure CosmosDB is used for:

- Scalable document storage
- Multi-region replication
- Automatic indexing
- Low-latency data access
- Flexible schema design

#### 3.5 Event Bus

Facilitates asynchronous communication between services:

- Event publishing
- Event subscription
- Message reliability
- Dead letter queuing
- Message replay capabilities

#### 3.6 React Frontend

Provides a responsive user interface:

- Real-time dashboards
- Interactive visualizations
- Responsive design
- Progressive web app capabilities
- Theme customization

## 4. Setup Instructions

### Prerequisites

- .NET 8 SDK
- Node.js (v18+)
- Docker Desktop
- Azure CLI (for CosmosDB emulator setup)
- Git

### Local Development Setup

#### 4.1 Clone the Repository

```bash
git clone https://github.com/your-org/rocketops.git
cd rocketops
```

#### 4.2 Setup Environment

```bash
# Copy example environment files
cp .env.example .env
```

#### 4.3 Start Development Environment with Docker

```bash
docker-compose up -d
```

This will start:

- All microservices
- API Gateway
- CosmosDB emulator
- Message broker
- Frontend development server

#### 4.4 Initialize Database

```bash
# Run database migrations
dotnet run --project ./tools/DbMigrator/DbMigrator.csproj
```

#### 4.5 Access the Application

- Frontend: <http://localhost:3000>
- Swagger UI: <http://localhost:8080/swagger>
- Individual service Swagger endpoints:
  - Monitoring API: <http://localhost:5001/swagger>
  - Alerts API: <http://localhost:5002/swagger>
  - Reporting API: <http://localhost:5003/swagger>

### Manual Setup (Without Docker)

For detailed instructions on setting up services individually, refer to the [Manual Setup Guide](./docs/manual-setup.md).

## 5. Testing Approach

RocketOps implements a comprehensive testing strategy to ensure quality and reliability:

### 5.1 Unit Tests

- Focus on testing individual components in isolation
- Mock external dependencies using NSubstitute
- Target high code coverage for domain and application layers
- Run as part of CI pipeline

```bash
# Run all unit tests
dotnet test ./tests/UnitTests
```

### 5.2 Integration Tests

- Test interaction between components
- Use test containers for external dependencies
- Focus on repository implementations and service integrations
- Verify CQRS command/query handling

```bash
# Run all integration tests
dotnet test ./tests/IntegrationTests
```

### 5.3 End-to-End Tests

- Use Playwright for browser automation
- Test complete user journeys
- Verify frontend-backend integration
- Run against isolated test environment

```bash
# Install Playwright browsers
cd ./tests/E2ETests
npm install
npx playwright install

# Run E2E tests
npx playwright test
```

### 5.4 Performance Tests

- Load testing with k6
- Benchmark critical operations
- Verify scalability under load
- Run as part of release pipeline

## 6. Design Decisions and Patterns

### 6.1 Clean Architecture

RocketOps follows Clean Architecture principles to maintain a clear separation of concerns:

- **Core Domain**: Contains business logic, entities, and interfaces
- **Application Layer**: Implements use cases through CQRS
- **Infrastructure Layer**: Provides technical implementations
- **Presentation Layer**: Handles user interaction

Benefits:

- Independence from frameworks
- Testability
- Separation of concerns
- Dependency rule enforcement

### 6.2 CQRS (Command Query Responsibility Segregation)

- **Commands**: Handle write operations and state changes
- **Queries**: Handle read operations with optimized data access

Implementation details:

- MediatR for in-process messaging
- Separate command and query handlers
- Validation using FluentValidation
- Response caching for queries

### 6.3 Event-Driven Architecture

- Services communicate via events
- Loose coupling between components
- Improved system resilience
- Better scalability

Event flow:

1. Services publish domain events
2. Event bus distributes events
3. Subscribers process events asynchronously

### 6.4 API Design

- RESTful API design with resource-based routing
- FastEndpoints for streamlined endpoint definition
- Versioning support
- Comprehensive documentation with Swagger
- Standardized response formats
- Problem Details for error responses (RFC 7807)

### 6.5 Security Considerations

- JWT-based authentication
- Role-based authorization
- Data encryption at rest and in transit
- Rate limiting
- Input validation and sanitization
- Audit logging

## 7. Contributing

Please read our [Contributing Guidelines](./CONTRIBUTING.md) for details on submitting pull requests.

## 8. License

This project is licensed under the MIT License - see the [LICENSE](./LICENSE) file for details.

## 9. Support

For support, please contact the maintainers or raise an issue in the GitHub repository.
