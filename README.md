# RocketOps - Deployment Health Monitor

## 1. Project Overview

RocketOps is a comprehensive Deployment Health Monitoring system designed to provide real-time insights into the health and performance of applications in production environments. Built on a microservices architecture, RocketOps offers robust monitoring capabilities, intelligent alerting, and detailed reporting to ensure optimal application performance and reliability.

## Current Status

- ✅ Solution architecture defined
- ✅ Project structure set up
- ✅ Shared libraries configuration
- ✅ Gateway API implementation completed
- ✅ Microservices API endpoints operational
- ✅ Swagger documentation integrated
- ✅ Docker containerization configured
- 🔄 Frontend implementation (in progress)
- ⏳ Testing implementation (planned)

## 2. Technology Stack

| Component | Technology |
|-----------|------------|
| Backend Services | .NET 8 using C# |
| API Framework | FastEndpoints |
| API Gateway | Ocelot |
| API Documentation | Swagger / OpenAPI |
| Frontend | React with TypeScript |
| Database | Azure CosmosDB |
| Communication | Event-driven messaging |
| Containerization | Docker & Docker Compose |
| Testing | xUnit, NSubstitute, Playwright |
| CI/CD | GitHub Actions |
| Monitoring | Application Insights |

## 3. Architecture

RocketOps follows a microservices architecture with clean architecture principles and CQRS pattern implementation:

### 3.1 Components

- **API Gateway (Ocelot)**: Routes requests, handles authentication, load balancing
- **Microservices**:
  - **Monitoring.API**: Health checks, metrics collection
  - **Alerts.API**: Alert management, notifications
  - **Reporting.API**: Analytics, report generation
- **Shared Libraries**: Core infrastructure, domain models, data access
- **Azure CosmosDB**: Persistent storage
- **React Frontend**: Dashboards and visualizations

## 4. Setup Instructions

### 4.1 Prerequisites

- .NET 8 SDK
- Node.js (v18+)
- Docker Desktop
- OpenSSL (for certificate generation)

### 4.2 Quick Start

#### 4.2.1 Generate Development Certificates

```bash
# Linux/macOS
./scripts/generate-certs.sh

# Windows
.\scripts\generate-certs.ps1
```

#### 4.2.2 Start Development Environment

```bash
docker-compose up -d
```

#### 4.2.3 Access the Application

- Frontend: <http://localhost:3000>
- Gateway API: <http://localhost:5000>
- Gateway Swagger: <http://localhost:5000/swagger>
- Monitoring API: <http://localhost:5010/swagger>
- Alerts API: <http://localhost:5020/swagger>
- Reporting API: <http://localhost:5030/swagger>
- CosmosDB Emulator: <https://localhost:8081/_explorer/index.html>

### 4.3 Project Structure

```
RocketOps/
├── src/
│   ├── Core/                    # Shared core libraries
│   │   ├── Application/         # CQRS, application services
│   │   ├── Domain/              # Domain models, events
│   │   └── Infrastructure/      # Cross-cutting concerns
│   ├── Gateway/                 # API Gateway
│   └── Services/
│       ├── Alerts.Api/          # Alerts microservice
│       ├── RocketOps.Monitoring.Api/  # Monitoring microservice
│       └── Reporting.Api/       # Reporting microservice
├── ui/
│   └── rocketops/               # React frontend
├── scripts/
│   ├── generate-certs.sh        # SSL certificate generation (Linux/macOS)
│   ├── generate-certs.ps1       # SSL certificate generation (Windows)
│   └── init-cosmos-db.js        # CosmosDB initialization script
├── certs/                       # Generated SSL certificates
├── docker-compose.yml           # Docker Compose configuration
└── README.md                    # This file
```

### 4.4 Docker Commands

#### 4.4.1 View Service Logs

```bash
# All services
docker-compose logs -f

# Specific service
docker-compose logs -f gateway
docker-compose logs -f alerts-service
docker-compose logs -f monitoring-service
docker-compose logs -f reporting-service
docker-compose logs -f frontend
docker-compose logs -f cosmosdb
```

#### 4.4.2 Restart and Rebuild

```bash
# Restart all services
docker-compose restart

# Rebuild all services
docker-compose up --build

# Rebuild specific service
docker-compose up --build gateway
```

#### 4.4.3 Stop Environment

```bash
# Stop containers
docker-compose down

# Stop and remove volumes
docker-compose down -v
```

### 4.5 SSL Certificates

The application uses self-signed SSL certificates for local development. After generating certificates, add the CA certificate to your trusted roots:

#### 4.5.1 Windows

1. Double-click on `certs/ca.crt`
2. Select "Install Certificate" → "Local Machine" → "Trusted Root Certification Authorities"

#### 4.5.2 macOS

```bash
sudo security add-trusted-cert -d -r trustRoot -k /Library/Keychains/System.keychain ./certs/ca.crt
```

#### 4.5.3 Linux (Ubuntu/Debian)

```bash
sudo cp ./certs/ca.crt /usr/local/share/ca-certificates/
sudo update-ca-certificates
```

### 4.6 Troubleshooting

### 4.6.1 Swagger Not Loading

## If Swagger UI returns a 500 error

- Check service logs: docker-compose logs [service-name]
- Verify Program.cs configuration has proper middleware ordering
- Ensure OpenAPI configuration is correct in appsettings.json

### 4.6.2 CosmosDB Connection Issues

- Check container status: docker-compose ps cosmosdb
- View logs: docker-compose logs cosmosdb
- Restart service: docker-compose restart cosmosdb
- Verify your host machine can access the emulator: <https://localhost:8081/_explorer/>

#### 4.6.3 Port Conflicts

Check for port availability:

```bash
# Windows
netstat -ano | findstr "5000 5001 3000 3001 8081"

# Linux/macOS
netstat -tuln | grep -E '5000|5001|3000|3001|8081'
```

### 4.7 Health Monitoring

Each service exposes a `/health` endpoint with:

- Service self-check status
- CosmosDB connectivity
- Dependent service status (for Gateway)

## 5. Testing Approach

(Testing implementation details will be added here)

## 6. Design Decisions and Patterns

- **Clean Architecture**: Separation of domain, application, and infrastructure concerns
- **CQRS**: Command/Query separation for optimized operations
- **Event-Driven Architecture**: Asynchronous communication between services
- **API Gateway Pattern**: Centralized request handling
- **Health Check Pattern**: Monitoring service status and dependencies
- **Options Pattern**: Consistent configuration across services
- **FastEndpoints Pattern**: Vertical slice architecture for API endpoints

## 7. Key Features

- **Real-time Monitoring**: Continuous tracking of application metrics
- **Intelligent Alerting**: Configurable thresholds with notifications
- **Comprehensive Reporting**: Performance analytics and trend analysis
- **Scalable Infrastructure**: Docker-based containerization
- **API Documentation**: Interactive Swagger UI for all endpoints
- **Containerized Development**: Consistent environment across all developers

<!-- ## 8. Contributing

(Contributing guidelines will be added here)

## 9. License

(License information will be added here) -->