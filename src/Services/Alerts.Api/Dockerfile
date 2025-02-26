# Use the generalized microservice Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Customize for Alerts service
ARG PROJECT_PATH=src/Services/Alerts.Api
ARG PROJECT_NAME=Alerts.Api

# Copy project files
COPY ["${PROJECT_PATH}/${PROJECT_NAME}.csproj", "project/"]

# Restore dependencies
WORKDIR /app/project
RUN dotnet restore "${PROJECT_NAME}.csproj"

# Copy the entire source code
COPY . /app

# Build the application
WORKDIR /app
RUN dotnet build "${PROJECT_PATH}/${PROJECT_NAME}.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "${PROJECT_PATH}/${PROJECT_NAME}.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy the published output
COPY --from=publish /app/publish .

# Set the entry point for Alerts service
ENTRYPOINT ["dotnet", "Alerts.Api.dll"]