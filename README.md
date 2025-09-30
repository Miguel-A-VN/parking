# Sistema de Gestión de Parqueadero

Sistema de gestión de parqueadero desarrollado en **.NET 9.0** y **C# 13.0**, utilizando **Entity Framework Core** para la gestión de datos y **SQL Server** como base de datos.

---

## 📦 Requisitos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- SQL Server (Local o Remoto)
- Visual Studio 2022 / VS Code (opcional)
- Navegador moderno para probar la aplicación (Chrome, Edge, Firefox)

---

## 🗂 Estructura del proyecto

```
Parking/
│
├─ Controllers/ # Controladores MVC
├─ Data/ # DbContext y configuraciones EF Core
├─ Models/ # Entidades y modelos de datos
├─ Views/ # Vistas Razor
│ ├─ Home/
│ └─ UserEntities/
├─ Migrations/ # Migraciones EF Core
├─ wwwroot/ # Archivos estáticos (CSS, JS, imágenes)
├─ Parking.csproj
└─ Program.cs
```


---

## ⚙️ Migraciones de la base de datos

### Crear migraciones

1. Navegar a la carpeta del proyecto:

```bash
cd .\Parking\
```

### Crear Migracion inicial
```bash
dotnet ef migrations add InitialCreate
```

### Aplicar migraciones

```bash
dotnet ef database update

```
Esto creará la base de datos con las tablas según el modelo actual.

## Reiniciar migraciones (desarrollo)

Si quieres eliminar todas las migraciones y reiniciar la base de datos:

```bash
dotnet ef database drop   # Elimina la base de datos
Remove-Item -Recurse .\Migrations\*  # Borra las migraciones (PowerShell)
dotnet ef migrations add InitialCreate
dotnet ef database update
```

⚠️ Solo recomendable en entornos de desarrollo, perderás todos los datos existentes.

## 🚀 Ejecutar la aplicación

### Inicia la aplicación desde Visual Studio o con:

```bash
dotnet run
```


**Abrir el navegador en:**

https://localhost:7273/Home/Register


Crear un usuario de Funcionario usando el código de registro:

Código: FUNC2025


## 🖥 Dashboard Funcionario

**El Dashboard del Funcionario permite:**

Administrar usuarios registrados en el sistema.

Crear, actualizar y eliminar usuarios.

Redirigirse a una vista para crear un nuevo usuario con rol Aprendiz.


## 🔑 Notas importantes

Este proyecto está pensado para entornos de desarrollo.

En producción, manejar migraciones y actualizaciones de base de datos con cuidado para no perder información.

Puedes personalizar los roles y códigos de registro en HomeController según las necesidades del sistema.