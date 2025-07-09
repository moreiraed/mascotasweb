using Microsoft.AspNetCore.Mvc;
using MascotasWeb.Models;

namespace MascotasWeb.Controllers
{
  public class MascotasController : Controller
  {
    private readonly MascotasDbContext _context;  // Contexto para acceder a la base de datos

    public MascotasController(MascotasDbContext context)
    {
      _context = context;
    }

    // GET: Muestra la lista completa de mascotas
    public IActionResult Index()
    {
      var lista = _context.Mascotas.ToList(); // Obtiene todas las mascotas de la BD
      return View(lista); // Pasa la lista a la vista
    }

    // GET: Muestra el formulario para crear una nueva mascota
    public IActionResult Create()
    {
      return View(); // Vista con formulario vacío
    }

    // POST: Recibe los datos del formulario para crear mascota, incluida la imagen
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Mascota mascota, IFormFile imagenArchivo)
    {
      if (!ModelState.IsValid)
      {
        // Si el modelo no es válido, muestra errores en consola y devuelve formulario con datos
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
          Console.WriteLine(error.ErrorMessage);
        }
        return View(mascota);
      }

      if (imagenArchivo != null && imagenArchivo.Length > 0)
      {
        // Carpeta donde se guardarán imágenes subidas por el usuario
        var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/uploads");
        Directory.CreateDirectory(rutaCarpeta); // Crea carpeta si no existe

        // Nombre único para evitar colisiones de archivos
        var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(imagenArchivo.FileName);
        var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

        // Guardamos la imagen en disco
        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        {
          await imagenArchivo.CopyToAsync(stream);
        }

        // Guardamos ruta relativa para mostrar la imagen en la vista
        mascota.Imagen = "/img/uploads/" + nombreArchivo;
      }
      else
      {
        // Si no se subió imagen, asigna imagen por defecto
        mascota.Imagen = "/img/static/defaultPetAvatar.jpeg";
      }

      _context.Add(mascota); // Añadimos la mascota a la BD
      await _context.SaveChangesAsync(); // Guardamos cambios en BD
      return RedirectToAction(nameof(Index)); // Redirige a la lista de mascotas
    }

    // GET: Muestra formulario para editar mascota según su id
    public IActionResult Edit(int id)
    {
      var mascota = _context.Mascotas.FirstOrDefault(m => m.Id == id);
      if (mascota == null) return NotFound(); // Retorna 404 si no existe
      return View(mascota); // Muestra formulario con datos actuales
    }

    // POST: Recibe datos para actualizar mascota, incluida posible nueva imagen
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Mascota mascota, IFormFile imagenArchivo)
    {
      var existente = _context.Mascotas.FirstOrDefault(m => m.Id == mascota.Id);
      if (existente == null) return NotFound();

      // Actualiza campos editables
      existente.Nombre = mascota.Nombre;
      existente.Especie = mascota.Especie;
      existente.Raza = mascota.Raza;
      existente.Color = mascota.Color;
      existente.FechaNacimiento = mascota.FechaNacimiento;
      existente.Descripcion = mascota.Descripcion;

      if (imagenArchivo != null && imagenArchivo.Length > 0)
      {
        var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/uploads");
        Directory.CreateDirectory(rutaCarpeta);

        // Borra imagen anterior si no es la imagen por defecto
        if (!string.IsNullOrEmpty(existente.Imagen) &&
            !existente.Imagen.Contains("defaultPetAvatar.jpeg"))
        {
          var rutaAnterior = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existente.Imagen.TrimStart('/'));
          if (System.IO.File.Exists(rutaAnterior))
          {
            System.IO.File.Delete(rutaAnterior);
          }
        }

        // Guarda nueva imagen con nombre único
        var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(imagenArchivo.FileName);
        var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        {
          await imagenArchivo.CopyToAsync(stream);
        }

        existente.Imagen = "/img/uploads/" + nombreArchivo; // Actualiza ruta imagen
      }

      await _context.SaveChangesAsync(); // Guarda cambios en BD
      return RedirectToAction("Index"); // Redirige a la lista de mascotas
    }

    // POST: Elimina una mascota y su imagen personalizada si la tiene
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
      var mascota = _context.Mascotas.FirstOrDefault(m => m.Id == id);
      if (mascota != null)
      {
        // Borra imagen personalizada si existe y no es la imagen por defecto
        if (!string.IsNullOrEmpty(mascota.Imagen) &&
            !mascota.Imagen.Contains("defaultPetAvatar.jpeg"))
        {
          var rutaImagen = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", mascota.Imagen.TrimStart('/'));
          if (System.IO.File.Exists(rutaImagen))
          {
            System.IO.File.Delete(rutaImagen);
          }
        }

        _context.Mascotas.Remove(mascota); // Elimina de la BD
        _context.SaveChanges(); // Guarda cambios en BD
      }

      return RedirectToAction("Index"); // Redirige a lista de mascotas
    }
  }
}
