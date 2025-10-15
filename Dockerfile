FROM nexus3.dotin.ir:7070/custome-images/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM nexus3.dotin.ir:7070/custome-images/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
COPY ["NuGet.Config", "."]
RUN dotnet restore "src/3.Endpoints/API/Master.Data.Endpoints.API.csproj" --verbosity normal
COPY . .
WORKDIR "/src/src/3.Endpoints/API"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Master.Data.Endpoints.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Master.Data.Endpoints.API.dll"]
