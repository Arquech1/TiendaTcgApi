using System.ComponentModel.DataAnnotations;

namespace TiendaTcgApi.Entidades
{
    public class Categoria
    {
        public int id { get; set; }
        [Required]
        public required string nombre { get; set; }
        public List<Producto> productos { get; set; } = new List<Producto>();
    }
}
