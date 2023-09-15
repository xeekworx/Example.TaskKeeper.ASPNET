#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TaskKeeper.Web/TaskKeeper.Web.csproj", "TaskKeeper.Web/"]
RUN dotnet restore "TaskKeeper.Web/TaskKeeper.Web.csproj"
COPY . .
WORKDIR "/src/TaskKeeper.Web"
RUN dotnet build "TaskKeeper.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskKeeper.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskKeeper.Web.dll"]