# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/Posterr.API/Posterr.API.csproj", "src/Posterr.API/"]
COPY ["src/Posterr.Application/Posterr.Application.csproj", "src/Posterr.Application/"]
COPY ["src/Posterr.Domain/Posterr.Domain.csproj", "src/Posterr.Domain/"]
COPY ["src/Posterr.Infrastructure/Posterr.Infrastructure.csproj", "src/Posterr.Infrastructure/"]

RUN dotnet restore "src/Posterr.API/Posterr.API.csproj"

COPY . .

WORKDIR "/src/src/Posterr.API"
RUN dotnet build "Posterr.API.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Posterr.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

EXPOSE 8080

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Posterr.API.dll"]