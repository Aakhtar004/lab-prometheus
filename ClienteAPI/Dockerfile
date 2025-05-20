##################################################
# STAGE 1: build the application
##################################################
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos el .csproj y restauramos dependencias
COPY ["ClienteAPI/ClienteAPI.csproj", "./ClienteAPI.csproj"]
RUN dotnet restore "ClienteAPI.csproj"

# Copiamos el resto de archivos y publicamos
COPY . .

# Forzamos que no se genere AssemblyInfo automáticamente
RUN dotnet publish "ClienteAPI.csproj" \
    -c Release -o /app/publish \
    -p:GenerateAssemblyInfo=false


##################################################
# STAGE 2: runtime image
##################################################
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ClienteAPI.dll"]
