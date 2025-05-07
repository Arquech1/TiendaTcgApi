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

        public ICollection<Categoria> Categorias { get; set; }

        [Unicode(false)]
        public string? foto { get; set; }
    }
}
