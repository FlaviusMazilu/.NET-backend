FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MobyLabWebProgramming.sln", "./"]
COPY ["MobyLabWebProgramming.Backend/", "./MobyLabWebProgramming.Backend"]
COPY ["MobyLabWebProgramming.Core/", "./MobyLabWebProgramming.Core"]
COPY ["MobyLabWebProgramming.Infrastructure/", "./MobyLabWebProgramming.Infrastructure"]

RUN dotnet restore "MobyLabWebProgramming.sln"

COPY . .
RUN dotnet publish "MobyLabWebProgramming.Backend\MobyLabWebProgramming.Backend.csproj" -c Release -o /app/publish