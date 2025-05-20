##################################################
# STAGE 1: build the application
##################################################
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos el .csproj y restauramos dependencias
COPY ["ClienteAPI/ClienteAPI.csproj", "./ClienteAPI.csproj"]
RUN dotnet restore "ClienteAPI.csproj"

# Copiamos todo el proyecto
COPY . .

# 1) Limpiamos posibles artefactos (obj/bin) que contengan attributes generados
RUN rm -rf obj bin

# 2) Publicamos sin regenerar AssemblyInfo
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
