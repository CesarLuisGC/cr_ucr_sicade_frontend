FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["App/App.csproj", "App/"]
COPY ["App/Views", "App/Views/"]
COPY ["../Business/Business.csproj", "Business/"]
COPY ["../Entities/Entities.csproj", "Entities/"]
COPY ["../Data/Data.csproj", "Data/"]
RUN dotnet restore "App/App.csproj"

COPY . .

WORKDIR "/src/App"
RUN dotnet build "App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "App.dll"]