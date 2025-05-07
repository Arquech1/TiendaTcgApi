using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaTcgApi.Data;
using TiendaTcgApi.DTOS;
using TiendaTcgApi.Entidades;
using TiendaTcgApi.Servicios;
using TiendaTcgApi.Utilidades;

namespace TiendaTcgApi.Controllers
{
    [ApiController]
    [Route("productos")]
    [Authorize(Policy = "esadmin")]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IAlmacenadorArchivox almacenadorArchivos;
        private readonly IMapper mapper;
        private const string contenedor = "productos";

        public ProductosController(AppDbContext context, IAlmacenadorArchivox almacenadorArchivos, IMapper mapper)
        {
            this.context = context;
            this.almacenadorArchivos = almacenadorArchivos;
            this.mapper = mapper;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Producto>> Get()
        {
            var productos = await context.Productos.ToListAsync();
            //var queryable = context.Productos.AsQueryable();
            //await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            //var autores = await queryable.OrderBy(x => x.nombre).Paginar(paginacionDTO).ToListAsync();
            //var productosDTO = mapper.Map<List<ProductoDTO>>(autores);
            return productos;

        }

        [HttpGet("{id:int}", Name = "ObtenerProducto")]
        [AllowAnonymous]
        public async Task<ActionResult<Producto>> GetId(int id)
        {
            var producto = await context.Productos.FirstOrDefaultAsync(x => x.id == id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> Post(Producto producto)
        {
            context.Add(producto);
            await context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerProducto", new { id = producto.id }, producto);
        }
        [HttpPost("conFoto")]
        public async Task<ActionResult<Producto>> FotoPost([FromForm] ProductoConFotoDTO productoDTO)
        {
            var producto = mapper.Map<Producto>(productoDTO);
            if(productoDTO.foto != null)
            {
                var url = await almacenadorArchivos.Almacenar(contenedor, productoDTO.foto);
                producto.foto = url;
            }
            else
            {
                producto.foto = null;
            }
            context.Add(producto);
            await context.SaveChangesAsync();
            var productoDto = mapper.Map<ProductoConFotoDTO>(producto); 
            return CreatedAtRoute("ObtenerProducto", new { id = producto.id }, productoDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id,[FromForm] ProductoConFotoDTO productoDTO)
        {

            var producto = mapper.Map<Producto>(productoDTO);
            if (producto.id != id)
            {
                return BadRequest();
            }
            if(producto.foto != null)
            {
                var fotoActual = await context.Productos
                    .Where(x => x.id == id)
                    .Select(x => x.foto)
                    .FirstOrDefaultAsync();

                var url = await almacenadorArchivos.Editar(contenedor, productoDTO.foto!, fotoActual);
            }
            context.Update(producto);
            await context.SaveChangesAsync();
            return NoContent();



        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var producto = await context.Productos.FirstOrDefaultAsync(x => x.id == id);

            if (producto == null)
            {
                return NotFound();
            }

            context.Remove(producto);

            await context.SaveChangesAsync();

            await almacenadorArchivos.Borrar(producto.foto, contenedor);

            return NoContent();
        }
    }
}
