# ===================== Etapa de compilación =====================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia el archivo .csproj de tu proyecto y restaura las dependencias
COPY LabCiberSeguridad.csproj ./
RUN dotnet restore "LabCiberSeguridad.csproj"

# Copia el resto del código y publica la aplicación
COPY . .
RUN dotnet publish "LabCiberSeguridad.csproj" -c Release -o /app/publish --no-restore

# ===================== Etapa de ejecución =====================
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

# Configuración del puerto (ideal para Render u otros servicios en la nube)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "LabCiberSeguridad.dll"]