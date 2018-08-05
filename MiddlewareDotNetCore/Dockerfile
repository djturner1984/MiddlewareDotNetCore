FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY MiddlewareDotNetCore/MiddlewareDotNetCore.csproj MiddlewareDotNetCore/
RUN dotnet restore MiddlewareDotNetCore/MiddlewareDotNetCore.csproj
COPY . .
WORKDIR /src/MiddlewareDotNetCore
RUN dotnet build MiddlewareDotNetCore.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MiddlewareDotNetCore.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MiddlewareDotNetCore.dll"]
