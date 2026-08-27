# Etapa 1: Construcción (Build)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar el archivo del proyecto y restaurar dependencias
COPY ["Perfiles.csproj", "./"]
RUN dotnet restore "./Perfiles.csproj"

# Copiar el resto del código fuente
COPY . .

# Compilar y publicar la aplicación en modo Release
RUN dotnet publish "./Perfiles.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: Producción (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

# Render requiere que la aplicación escuche en un puerto expuesto.
# Configuramos ASP.NET Core para que escuche en el puerto 8080 (predeterminado en .NET 8+)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# === SOLUCIÓN AL ERROR DE INOTIFY ===
# Desactivar la recarga automática de configuraciones
ENV DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE=false

# Copiar los archivos compilados desde la etapa de construcción
COPY --from=build /app/publish .

# Comando de inicio de la aplicación
ENTRYPOINT ["dotnet", "Perfiles.dll"]