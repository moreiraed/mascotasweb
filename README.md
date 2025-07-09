
# MascotasWeb

Aplicación web ASP.NET Core para administrar un listado de mascotas. Permite crear, editar, eliminar mascotas y subir imágenes personalizadas para cada una.

## Tecnologías

- .NET 8  
- ASP.NET Core MVC  
- Entity Framework Core con SQLite  

## Estructura general

- `/Controllers`: Controladores MVC (ej. MascotasController)  
- `/Models`: Modelos y contexto de EF Core  
- `/Views`: Vistas Razor  
- `/wwwroot`: Archivos estáticos (css, js, imágenes)  
- `mascotas.db`: Base de datos SQLite (NO versionada)  

## Configuración y uso

1. Clonar el repositorio:

   ```bash
   git clone https://github.com/tu_usuario/tu_repositorio.git
   cd MascotasWeb
   ```

2. Restaurar paquetes NuGet:

   ```bash
   dotnet restore
   ```

3. Ejecutar migraciones (si no se tiene la base de datos local):

   ```bash
   dotnet ef database update
   ```

4. Levantar la aplicación:

   ```bash
   dotnet run
   ```

5. Abrir en navegador la URL que indica la consola, usualmente:

   ```
   https://localhost:5001
   ```

## Notas importantes

- La base de datos `mascotas.db` **no está versionada**. Cada máquina debe crearla ejecutando las migraciones.  
- Las imágenes que suben los usuarios se guardan en `wwwroot/img/uploads/`, carpeta que está ignorada en Git.  
- La imagen por defecto se encuentra en `wwwroot/img/static/defaultPetAvatar.jpeg`.  

---

## Cómo generar migraciones nuevas

Si realizás cambios en los modelos, podés crear migraciones y actualizar la base de datos con estos comandos:

```bash
dotnet ef migrations add NombreMigracion
dotnet ef database update
```

---

## Contacto

Cualquier duda o consulta, podés contactarme.
