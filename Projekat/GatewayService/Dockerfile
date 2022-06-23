FROM mcr.microsoft.com/dotnet/sdk:5.0 as build-env
WORKDIR /app

COPY *.sln .
COPY GatewayAPI/*.csproj ./GatewayAPI/
COPY GatewayLogic/*.csproj ./GatewayLogic/
COPY DataLayer/*.csproj ./DataLayer/

RUN dotnet restore

COPY GatewayAPI/. ./GatewayAPI/
COPY GatewayLogic/. ./GatewayLogic/
COPY DataLayer/. ./DataLayer/

WORKDIR /app/GatewayAPI
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/GatewayAPI/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "GatewayAPI.dll"]