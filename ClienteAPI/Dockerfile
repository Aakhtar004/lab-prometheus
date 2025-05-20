##################################################
# STAGE 1: build the application
##################################################
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos el .csproj y restauramos dependencias
COPY ["ClienteAPI.csproj", "./"]
RUN dotnet restore "ClienteAPI.csproj"

# Copiamos el resto de archivos y publicamos
COPY . .
RUN dotnet publish "ClienteAPI.csproj" -c Release -o /app/publish

##################################################
# STAGE 2: runtime image
##################################################
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ClienteAPI.dll"]
