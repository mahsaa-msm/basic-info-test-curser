FROM nexus3.dotin.ir:7070/custome-images/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM nexus3.dotin.ir:7070/custome-images/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
COPY ["NuGet.Config", "."]
RUN --mount=type=cache,target=/root/.nuget/packages \
    --mount=type=cache,target=/root/.local/share/NuGet/v3-cache \
    dotnet restore "src/3.Endpoints/API/Vehicle.Insurance.Endpoints.API.csproj" --verbosity normal
COPY . .
WORKDIR "/src/src/3.Endpoints/API"
RUN --mount=type=cache,target=/root/.nuget/packages \
    --mount=type=cache,target=/root/.local/share/NuGet/v3-cache \
    dotnet build -c Release -o /app/build

FROM build AS publish
RUN --mount=type=cache,target=/root/.nuget/packages \
    --mount=type=cache,target=/root/.local/share/NuGet/v3-cache \
    dotnet publish "Vehicle.Insurance.Endpoints.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vehicle.Insurance.Endpoints.API.dll"]

