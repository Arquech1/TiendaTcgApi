namespace TiendaTcgApi.DTOS
{
    public class ProductoConFotoDTO: ProductoDTO 
    {
        public IFormFile? foto { get; set; }
    }
}
