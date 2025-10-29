# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia só os arquivos .csproj primeiro (acelera cache)
COPY neuromasters.api/neuromasters.api.csproj neuromasters.api/
COPY neuromasters.borders/neuromasters.borders.csproj neuromasters.borders/
COPY neuromasters.gateway/neuromasters.gateway.csproj neuromasters.gateway/
COPY neuromasters.handlers/neuromasters.handlers.csproj neuromasters.handlers/
COPY neuromasters.repositories/neuromasters.repositories.csproj neuromasters.repositories/

RUN dotnet restore neuromasters.api/neuromasters.api.csproj

# Copia o restante do código
COPY . .

# Publica a API em modo Release
WORKDIR /src/neuromasters.api
RUN dotnet publish neuromasters.api.csproj -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: Runtime (roda a API)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# A API vai escutar na porta 8080 dentro do container
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1

COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "neuromasters.api.dll"]