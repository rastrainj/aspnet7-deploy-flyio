FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["aspnet7-deploy-flyio.csproj", "./"]
RUN dotnet restore "aspnet7-deploy-flyio.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "aspnet7-deploy-flyio.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "aspnet7-deploy-flyio.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "aspnet7-deploy-flyio.dll"]
