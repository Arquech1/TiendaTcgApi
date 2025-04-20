using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TiendaTcgApi.Entidades
{
    public class Producto
    {
        public int id { get; set; }

        [Required]
        public required string nombre { get; set; }

        [Unicode(false)]
        public string? foto { get; set; }
    }
}
