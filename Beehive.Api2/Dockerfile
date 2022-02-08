FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Beehive.Api2/Beehive.Api2.csproj", "Beehive.Api2/"]
RUN dotnet restore "Beehive.Api2/Beehive.Api2.csproj"
COPY . .
WORKDIR "/src/Beehive.Api2"
RUN dotnet build "Beehive.Api2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Beehive.Api2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Beehive.Api2.dll"]
