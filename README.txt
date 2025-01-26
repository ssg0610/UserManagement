Este proyecto consiste en una solución de gestión de usuarios y departamentos.
Incluye una API desarrollada en .NET 8 y un frontend desarrollado en Angular.

### Requisitos Previos
- .NET SDK 8.0 o superior
- Node.js (18.19.1 o superior) y npm (10.9.2 o superior)
- Angular CLI (instalado globalmente con `npm install -g @angular/cli`)
- Base de datos local de SQL Server Management ((localdb)\\mssqllocaldb)

### Configuración del Backend (.NET)
- Importar la base de dato en SQL Server Management
	- Haz click derecho en Bases de Datos y selecciona Restaurar Base de Datos
	- Selecciona la opción de Dispositivo y carga el archivo .back 
	- Configura las opciones de restauración y haz click en aceptar
- Navega al directorio del backend
- Restaurar las dependencias con el comando `dotnet restore`
- Ejecutar la API
	- Si cuenta con Visual Studio Profesional bastará con abrir la solución y ejecutarla
	- Mediante consola de comandos ejecutar el siguiente comando `dotnet run`
- La API estará disponible en HTTPS mediante la URL: https://localhost:7023, mediante HTTP: http://localhost:5126

### Configuración del Frontend (Angular)
- Navegar al directorio del frontend
- Restaurar las dependencias con el comando `npm i`
- Ejecutar la aplicación con el comando `ng serve --open`
- La aplicación estará disponible mediante la URL: http://localhost:4200

### Validación del proyecto
1. Prueba los endpoints de  la API usando Postman o cualquier cliente HTTP:
2. Usa la interfaz de usuario para gestionar usuarios y departamentos:
	- Valida que puedes crear, editar, eliminar y ver usuarios, y departamentos

### Estructura del Proyecto
- `/Back-End`: código fuente de la API desarrollada en .NET.
- `/Front-End`: código fuente del frontend desarrollado en Angular
- `/PruebaTecnica.Tests`: código de las pruebas unitarias del backend
- `README.md`: este archivo
- `/BBDD`: fichero para importar la base de datos
- `.git`: carpeta que contiene el repositorio

### Ejecución de Tests
- Backend: `dotnet test`
- Frontend: `ng test`