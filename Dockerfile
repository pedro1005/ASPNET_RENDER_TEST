# Use the ASP.NET runtime image for the base layer (where the app runs)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the .NET SDK for build and publish steps
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EisntMvc.csproj", "./"]
RUN dotnet restore "EisntMvc.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "./EisntMvc.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "./EisntMvc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Setup the final image based on the base layer
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables for your app configuration (e.g., PostgreSQL connection string)
# You can override these via Render's environment variables for production
ENV ASPNETCORE_ENVIRONMENT=Production
# Example for local testing, you should replace this or handle it in Render:
ENV ConnectionStrings__DefaultConnection="Host=dpg-d0f9ehpr0fns7397cui0-a.frankfurt-postgres.render.com;Port=5432;Database=gestor_clientes;Username=root;Password=yoBnbpLHsok1eQOUw1tQ6qYWUTiJ4nIM;SSL Mode=Require;Trust Server Certificate=true"

# Start the application
ENTRYPOINT ["dotnet", "EisntMvc.dll"]

