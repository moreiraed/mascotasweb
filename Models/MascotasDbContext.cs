using Microsoft.EntityFrameworkCore;

namespace MascotasWeb.Models
{
    public class MascotasDbContext : DbContext
    {
        public MascotasDbContext(DbContextOptions<MascotasDbContext> options)
            : base(options)
        {
        }

        public DbSet<Mascota> Mascotas { get; set; }
    }
}