# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 7070
EXPOSE 7071
ARG ASPNETCORE_ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT
ENV ASPNETCORE_HTTP_PORTS=7070


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ApiAfiliados/Afonso-Api.csproj", "ApiAfiliados/"]
COPY ["/Dominio/Dominio.csproj", "Dominio/"]
COPY ["/Servico/Servico.csproj", "Servico/"]
RUN dotnet restore "./ApiAfiliados/Afonso-Api.csproj"
COPY . .
WORKDIR "/src/ApiAfiliados"
RUN dotnet build "./Afonso-Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Afonso-Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Afonso-Api.dll"]
