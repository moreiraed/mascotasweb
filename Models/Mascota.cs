namespace MascotasWeb.Models
{

    public enum Especie
    {
        Perro,
        Gato,
        Otro
    }
    public class Mascota
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public Especie Especie { get; set; }
        public string? Raza { get; set; }
        public string? Color { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Imagen { get; set; }
        public string? Descripcion { get; set; }
    }
}
