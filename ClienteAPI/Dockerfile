##################################################
# STAGE 1: build the application
##################################################
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1) Copiamos sólo el .csproj (ahora directo en el contexto)
COPY ["ClienteAPI.csproj", "./"]
RUN dotnet restore "ClienteAPI.csproj"

# 2) Copiamos todo el código
COPY . .

# 3) Limpiamos artefactos obj/bin que puedan traer atributos duplicados
RUN rm -rf obj bin

# 4) Publicamos sin regenerar AssemblyInfo
RUN dotnet publish "ClienteAPI.csproj" \
    -c Release \
    -o /app/publish \
    -p:GenerateAssemblyInfo=false

##################################################
# STAGE 2: runtime image
##################################################
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# 5) Copiamos el output publicado
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ClienteAPI.dll"]
