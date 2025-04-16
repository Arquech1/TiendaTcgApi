using Microsoft.EntityFrameworkCore;
using TiendaTcgApi.Entidades;

namespace TiendaTcgApi.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        DbSet<Producto> Productos { get; set; } = null!;
    }
}
