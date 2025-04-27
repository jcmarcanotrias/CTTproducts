# Usamos una imagen base de runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Establecemos el directorio de trabajo dentro del contenedor
WORKDIR /app

# Copiamos el artefacto publicado desde el directorio de tu proyecto
COPY ./publish .

# Exponemos el puerto donde la app estará disponible
EXPOSE 80

# Establecemos el comando de inicio de la aplicación
ENTRYPOINT ["dotnet", "CTTproducts.dll"]