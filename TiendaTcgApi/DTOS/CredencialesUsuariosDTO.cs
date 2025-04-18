using System.ComponentModel.DataAnnotations;

namespace TiendaTcgApi.DTOS
{
    public class CredencialesUsuariosDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public string? Password { get; set; } 
    }
}
