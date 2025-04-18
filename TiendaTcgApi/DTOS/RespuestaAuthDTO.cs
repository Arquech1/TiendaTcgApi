namespace TiendaTcgApi.DTOS
{
    public class RespuestaAuthDTO
    {
        public required string Token { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }
}
