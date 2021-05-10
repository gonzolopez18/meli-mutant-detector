#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MutantDetector.Api/MutantDetector.Api.csproj", "MutantDetector.Api/"]
COPY ["MutantDetector.Domain/MutantDetector.Domain.csproj", "MutantDetector.Domain/"]
COPY ["MutantDetector.Infraestructure/MutantDetector.Infraestructure.csproj", "MutantDetector.Infraestructure/"]
RUN dotnet restore "MutantDetector.Api/MutantDetector.Api.csproj"
COPY . .
WORKDIR "/src/MutantDetector.Api"
RUN dotnet build "MutantDetector.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MutantDetector.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MutantDetector.Api.dll"]