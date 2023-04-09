FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Dominos.Api/Dominos.Api.csproj", "Dominos.Api/"]
RUN dotnet restore "Dominos.Api/Dominos.Api.csproj"
COPY . .
WORKDIR "/src/Dominos.Api"
RUN dotnet build "Dominos.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dominos.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dominos.Api.dll"]
