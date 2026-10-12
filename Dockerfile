# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Copy the solution file and restore dependencies for the entire solution.
COPY Menu.sln ./
# Copy the project files required for dependency restoration.
COPY ["src/Api/Api.csproj", "src/Api/"]
COPY ["src/Common/Common.csproj", "src/Common/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infra/Infra.csproj", "src/Infra/"]
COPY ["tests/Tests.Acceptance/Tests.Acceptance.csproj", "tests/Tests.Acceptance/"]
COPY ["tests/Tests.Shared/Tests.Shared.csproj", "tests/Tests.Shared/"]

# Restore NuGet packages (this uses the solution file to restore dependencies for all projects)
RUN dotnet restore "Menu.sln"

# Copy the rest of the source code into the container.
COPY . .

# Build and publish the Web project (adjust configuration and output path as needed).
RUN dotnet publish "src/Api/Api.csproj" -c Release -o /app/publish

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Api.dll"]
