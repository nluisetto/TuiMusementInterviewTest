FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TuiMusement.TwoDaysWeatherForecasting.ConsoleApp/TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.csproj", "TuiMusement.TwoDaysWeatherForecasting.ConsoleApp/"]
COPY ["TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests/TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests.csproj", "TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests/"]
RUN dotnet restore "TuiMusement.TwoDaysWeatherForecasting.ConsoleApp/TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.csproj"
COPY . .
WORKDIR "/src/TuiMusement.TwoDaysWeatherForecasting.ConsoleApp"
RUN dotnet build "TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.csproj" -c Release -o /app/build

FROM build AS tests
WORKDIR /src
RUN dotnet restore "TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests/TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests.csproj"
WORKDIR "/src/TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Tests"
RUN dotnet test --logger:trx

FROM build AS publish
RUN dotnet publish "TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.dll"]
