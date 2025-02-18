# High Level Architecture

graph TB
    subgraph "Client Layer"
        Client["React Frontend"]
    end

    subgraph "API Gateway"
        Gateway["Ocelot API Gateway"]
    end

    subgraph "Microservices"
        subgraph "Monitoring.API"
            MonAPI["Monitoring Service"]
            MonAPI_CQRS["CQRS Components"]
        end
        
        subgraph "Alerts.API"
            AlertAPI["Alerts Service"]
            AlertAPI_CQRS["CQRS Components"]
        end
        
        subgraph "Reporting.API"
            ReportAPI["Reporting Service"]
            ReportAPI_CQRS["CQRS Components"]
        end
    end
    
    subgraph "Shared Libraries"
        Infrastructure["Infrastructure Library"]
        Domain["Domain Library"]
        Data["Data Access Library"]
    end
    
    subgraph "Persistence Layer"
        CosmosDB[("CosmosDB")]
    end
    
    subgraph "Messaging"
        EventBus["Event Bus"]
    end
    
    subgraph "Docker Environment"
        Docker["Docker Compose"]
    end
    
    subgraph "Testing"
        UnitTests["Unit Tests"]
        IntegrationTests["Integration Tests"]
        E2ETests["E2E Tests (Playwright)"]
    end
    
    %% Connections
    Client --> Gateway
    Gateway --> MonAPI
    Gateway --> AlertAPI
    Gateway --> ReportAPI
    
    MonAPI --> MonAPI_CQRS
    AlertAPI --> AlertAPI_CQRS
    ReportAPI --> ReportAPI_CQRS
    
    MonAPI_CQRS --> Infrastructure
    AlertAPI_CQRS --> Infrastructure
    ReportAPI_CQRS --> Infrastructure
    
    MonAPI_CQRS --> Domain
    AlertAPI_CQRS --> Domain
    ReportAPI_CQRS --> Domain
    
    Infrastructure --> Data
    Data --> CosmosDB
    
    MonAPI --> EventBus
    AlertAPI --> EventBus
    ReportAPI --> EventBus
    EventBus --> MonAPI
    EventBus --> AlertAPI
    EventBus --> ReportAPI
    
    Docker -.-> Client
    Docker -.-> Gateway
    Docker -.-> MonAPI
    Docker -.-> AlertAPI
    Docker -.-> ReportAPI
    Docker -.-> CosmosDB
    Docker -.-> EventBus
    
    UnitTests -.-> Domain
    UnitTests -.-> MonAPI
    UnitTests -.-> AlertAPI
    UnitTests -.-> ReportAPI
    
    IntegrationTests -.-> MonAPI
    IntegrationTests -.-> AlertAPI
    IntegrationTests -.-> ReportAPI
    
    E2ETests -.-> Client
    
    classDef microservice fill:#b3e0ff,stroke:#0066cc
    classDef library fill:#ffdf9e,stroke:#996600
    classDef database fill:#d9b3ff,stroke:#6600cc
    classDef client fill:#c6ecc6,stroke:#2d882d
    classDef gateway fill:#ffb3b3,stroke:#cc0000
    classDef messaging fill:#ffd9b3,stroke:#cc6600
    classDef testing fill:#e6e6e6,stroke:#666666
    classDef docker fill:#d9d9d9,stroke:#4d4d4d
    classDef cqrs fill:#ffc6e0,stroke:#cc0066
    
    class Client client
    class Gateway gateway
    class MonAPI,AlertAPI,ReportAPI microservice
    class MonAPI_CQRS,AlertAPI_CQRS,ReportAPI_CQRS cqrs
    class Infrastructure,Domain,Data library
    class CosmosDB database
    class EventBus messaging
    class UnitTests,IntegrationTests,E2ETests testing
    class Docker docker
