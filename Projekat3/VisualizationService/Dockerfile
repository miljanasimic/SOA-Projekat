FROM mcr.microsoft.com/dotnet/sdk:5.0 as build-env
WORKDIR /app

COPY *.sln .
COPY VisualizationService/*.csproj ./VisualizationService/

RUN dotnet restore

COPY VisualizationService/. ./VisualizationService/
COPY ./dashboards/. ./dashboards/

WORKDIR /app/VisualizationService
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/VisualizationService/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "VisualizationService.dll"]