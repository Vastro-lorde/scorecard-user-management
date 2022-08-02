#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["scorecard-user-mgt/scorecard-user-mgt.csproj", "scorecard-user-mgt/"]
RUN dotnet restore "scorecard-user-mgt/scorecard-user-mgt.csproj"
COPY . .
WORKDIR "/src/scorecard-user-mgt"
RUN dotnet build "scorecard-user-mgt.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "scorecard-user-mgt.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "scorecard-user-mgt.dll"]
