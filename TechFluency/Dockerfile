# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar csproj e restaurar dependências
COPY TechFluency/TechFluency.csproj ./TechFluency/
RUN dotnet restore ./TechFluency/TechFluency.csproj

# Copiar tudo e fazer build
COPY TechFluency/. ./TechFluency/
WORKDIR /src/TechFluency
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "TechFluency.dll"]
