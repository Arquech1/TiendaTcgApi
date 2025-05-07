using System.ComponentModel.DataAnnotations;

namespace TiendaTcgApi.Entidades
{
    public class Categoria
    {
        public int id { get; set; }
        [Required]
        public required string nombre { get; set; }

        public ICollection<Producto> Productos { get; set; } 

    }
}
