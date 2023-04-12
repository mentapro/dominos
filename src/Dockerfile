FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Dominos.Api/Dominos.Api.csproj", "Dominos.Api/"]
COPY ["Dominos.Domain/Dominos.Domain.csproj", "Dominos.Domain/"]
COPY ["Dominos.Persistence.Abstractions/Dominos.Persistence.Abstractions.csproj", "Dominos.Persistence.Abstractions/"]
COPY ["Dominos.Persistence.Postgres/Dominos.Persistence.Postgres.csproj", "Dominos.Persistence.Postgres/"]
RUN dotnet restore "Dominos.Api/Dominos.Api.csproj"
COPY . .
RUN ls
WORKDIR "/src/Dominos.Api"
RUN dotnet build "Dominos.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dominos.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dominos.Api.dll"]
