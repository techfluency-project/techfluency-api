# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia os arquivos do projeto e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia tudo e faz o build em modo Release
COPY . ./
RUN dotnet publish -c Release -o out

# Etapa 2: Runtime da aplicação
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Porta padrão para ASP.NET Core
EXPOSE 80

# Comando de execução
ENTRYPOINT ["dotnet", "TechFluency.dll"]
