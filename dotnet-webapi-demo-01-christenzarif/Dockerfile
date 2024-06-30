#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["dotnet-webapi-demo-01-christenzarif.csproj", "."]
RUN dotnet restore "./dotnet-webapi-demo-01-christenzarif.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./dotnet-webapi-demo-01-christenzarif.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./dotnet-webapi-demo-01-christenzarif.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables
ENV DB_SERVER=tcp:casetrue.database.windows.net,1433
ENV DB_DATABASE=casetrue
ENV DB_USER=casetrue
ENV DB_PASSWORD=kiY8v6IVq~4&lG

ENTRYPOINT ["dotnet", "dotnet-webapi-demo-01-christenzarif.dll"]