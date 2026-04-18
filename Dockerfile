FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier SEULEMENT les fichiers .csproj d'abord (cache layer)
COPY *.csproj .
RUN dotnet restore --disable-parallel

# Ensuite copier le reste du code
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "tp2.dll"]