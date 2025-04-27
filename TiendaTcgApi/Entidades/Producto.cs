using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TiendaTcgApi.Entidades
{
    public class Producto
    {
        public int id { get; set; }

        [Required]
        public required string nombre { get; set; }
        [Required]
        public required string descripcion { get; set; }
        [Required]
        public required decimal precio { get; set; }

        [Required]
        public int stock { get; set; }

        public List<Categoria> categoria { get; set; } = new List<Categoria>();

        [Unicode(false)]
        public string? foto { get; set; }
    }
}
