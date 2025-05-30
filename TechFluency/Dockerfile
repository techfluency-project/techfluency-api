# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar csproj e restaurar dependências
COPY TechFluency.csproj ./
RUN dotnet restore "./TechFluency.csproj"

# Copiar tudo e fazer o build
COPY . ./
RUN dotnet publish "./TechFluency.csproj" -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app

# Setar a URL para ouvir na porta do Render
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "TechFluency.dll"]
