FROM mcr.microsoft.com/dotnet/sdk:5.0 as build-env
WORKDIR /app

COPY *.sln .
COPY AnalyticsService/*.csproj ./AnalyticsService/
COPY GrpcClient/*.csproj ./GrpcClient/

RUN dotnet restore

COPY AnalyticsService/. ./AnalyticsService/
COPY GrpcClient/. ./GrpcClient/

WORKDIR /app/AnalyticsService
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/AnalyticsService/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "AnalyticsService.dll"]