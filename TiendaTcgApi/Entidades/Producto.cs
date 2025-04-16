using System.ComponentModel.DataAnnotations;

namespace TiendaTcgApi.Entidades
{
    public class Producto
    {
        public int id { get; set; }

        [Required]
        public required string nombre { get; set; }
    }
}
