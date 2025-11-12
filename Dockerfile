# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files for all projects
COPY BackendLab3.Api/*.csproj ./BackendLab3.Api/
COPY BackendLab3.Services/*.csproj ./BackendLab3.Services/
COPY BackendLab3.DataAccess/*.csproj ./BackendLab3.DataAccess/

# Restore dependencies for API project
RUN dotnet restore BackendLab3.Api/BackendLab3.Api.csproj

# Copy everything else
COPY . .

# Copy SQLite DB from DataAccess project
RUN mkdir -p /app/App_Data
COPY BackendLab3.DataAccess/data.db /app/App_Data/data.db
RUN chmod 777 /app/App_Data/data.db

# Publish API project
RUN dotnet publish BackendLab3.Api/BackendLab3.Api.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published API
COPY --from=build /app/publish .

# Expose ports
EXPOSE 5000
EXPOSE 5001

# Environment variables
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_URLS=http://+:5000;https://+:5001

# Run the API
ENTRYPOINT ["dotnet", "BackendLab3.Api.dll"]
