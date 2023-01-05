#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/BlackWell.API/BlackWell.API.csproj", "src/BlackWell.API/"]
RUN dotnet restore "src/BlackWell.API/BlackWell.API.csproj"
COPY . .
WORKDIR "/src/src/BlackWell.API"
RUN dotnet build "BlackWell.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlackWell.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlackWell.API.dll"]