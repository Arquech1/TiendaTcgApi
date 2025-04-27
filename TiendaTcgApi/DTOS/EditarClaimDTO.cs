using System.ComponentModel.DataAnnotations;

namespace TiendaTcgApi.DTOS
{
    public class EditarClaimDTO
    {
        [EmailAddress]
        [Required]
        public required string email { get; set; }
    }
}
