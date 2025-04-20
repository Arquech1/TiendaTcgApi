namespace TiendaTcgApi.Servicios
{
    public interface IAlmacenadorArchivox
    {
        Task Borrar(string? ruta, string contenedor);
        Task<string> Almacenar(string contenedor, IFormFile archivo);

        async Task<string> Editar(string contenedor, IFormFile archivo, string? ruta)
        {
            await Borrar(ruta, contenedor);
            return await Almacenar(contenedor, archivo);
        }
    }
}
