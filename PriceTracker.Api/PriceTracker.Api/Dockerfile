#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PriceTracker.Api/PriceTracker.Api.csproj", "PriceTracker.Api/"]
RUN dotnet restore "PriceTracker.Api/PriceTracker.Api.csproj"
COPY . .
WORKDIR "/src/PriceTracker.Api"
RUN dotnet build "PriceTracker.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PriceTracker.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PriceTracker.Api.dll"]