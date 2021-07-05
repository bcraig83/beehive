FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Beehive.Api/Beehive.Api.csproj", "Beehive.Api/"]
RUN dotnet restore "Beehive.Api/Beehive.Api.csproj"
COPY . .
WORKDIR "/src/Beehive.Api"
RUN dotnet build "Beehive.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Beehive.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Beehive.Api.dll"]
