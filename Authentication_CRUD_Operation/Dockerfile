# Use the official ASP.NET Core image as a base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy the published API files to the container
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# COPY ["./Authentication_CRUD_Operation.csproj.csproj", "src/"]
COPY . .
RUN dotnet restore "./Authentication_CRUD_Operation.csproj"
RUN dotnet build "Authentication_CRUD_Operation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authentication_CRUD_Operation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication_CRUD_Operation.dll"]
