#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["04_Facade/JHLA.Api/JHLA.Api.csproj", "04_Facade/JHLA.Api/"]
RUN dotnet restore "04_Facade/JHLA.Api/JHLA.Api.csproj"
COPY . .
WORKDIR "/src/04_Facade/JHLA.Api"
RUN dotnet build "JHLA.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JHLA.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JHLA.Api.dll"]