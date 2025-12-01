FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ProductService/ProductService.API/ProductService.API.csproj", "src/ProductService/ProductService.API/"]
COPY ["src/ProductService/ProductService.Application/ProductService.Application.csproj", "src/ProductService/ProductService.Application/"]
COPY ["src/ProductService/ProductService.Domain/ProductService.Domain.csproj", "src/ProductService/ProductService.Domain/"]
COPY ["src/Shared/Shared.csproj", "src/Shared/"]
COPY ["src/ProductService/ProductService.Infrastructure/ProductService.Infrastructure.csproj", "src/ProductService/ProductService.Infrastructure/"]
RUN dotnet restore "src/ProductService/ProductService.API/ProductService.API.csproj"
COPY . .
WORKDIR "/src/src/ProductService/ProductService.API"
RUN dotnet build "./ProductService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.API.dll"]
