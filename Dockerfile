# Use the official .NET image from the Microsoft Container Registry
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["dotnet-webapi-demo-01-christenzarif.csproj", "./"]
RUN dotnet restore "./dotnet-webapi-demo-01-christenzarif.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "dotnet-webapi-demo-01-christenzarif.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dotnet-webapi-demo-01-christenzarif.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables
ENV DB_SERVER=tcp:casetrue.database.windows.net,1433
ENV DB_DATABASE=casetrue
ENV DB_USER=casetrue
ENV DB_PASSWORD=kiY8v6IVq~4&lG

ENTRYPOINT ["dotnet", "dotnet-webapi-demo-01-christenzarif.dll"]
