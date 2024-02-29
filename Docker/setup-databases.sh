@echo off

REM Comprobar si los controladores de base de datos están instalados
REM Nota: Asegúrate de que los controladores estén instalados manualmente en Windows

REM Ejecutar los archivos docker-compose.yml para crear los servicios de base de datos
echo Creando contenedores de base de datos...
docker-compose -f postgres-compose.yml up -d
docker-compose -f mysql-compose.yml up -d
docker-compose -f mssql-compose.yml up -d

echo Contenedores de base de datos creados con éxito.
